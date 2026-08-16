using System.Linq;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace RapidEnum;

[Generator]
public class RapidEnumGenerator : IIncrementalGenerator
{
    public void Initialize(IncrementalGeneratorInitializationContext context)
    {
        context.RegisterPostInitializationOutput(static initContext =>
            initContext.AddSource("RapidEnumAttributes.g.cs", RapidEnumTemplate.GenerateAttributes()));

        // [RapidEnumWithType(typeof(SomeEnum))]
        RegisterOutput(context, context.SyntaxProvider.ForAttributeWithMetadataName(
            Constants.WithTypeAttributeFullName,
            static (node, token) =>
            {
                token.ThrowIfCancellationRequested();
                return node is ClassDeclarationSyntax;
            },
            static (attributeContext, token) =>
            {
                token.ThrowIfCancellationRequested();
                return CreateWithTypeContext(attributeContext);
            }));

        // [RapidEnum]
        RegisterOutput(context, context.SyntaxProvider.ForAttributeWithMetadataName(
            Constants.EnumAttributeFullName,
            static (node, token) =>
            {
                token.ThrowIfCancellationRequested();
                return node is EnumDeclarationSyntax;
            },
            static (attributeContext, token) =>
            {
                token.ThrowIfCancellationRequested();
                return CreateEnumContext(attributeContext);
            }));
    }

    private static void RegisterOutput(
        IncrementalGeneratorInitializationContext context,
        IncrementalValuesProvider<RapidEnumGeneratorContext?> provider)
    {
        context.RegisterSourceOutput(provider.Where(static x => x is not null),
            static (sourceContext, generationContext) =>
            {
                if (generationContext == null) return;

                if (generationContext.DiagnosticDescriptor != null)
                {
                    sourceContext.ReportDiagnostic(Diagnostic.Create(generationContext.DiagnosticDescriptor,
                        generationContext.DiagnosticLocation, generationContext.ClassName));
                    return;
                }

                sourceContext.AddSource(generationContext.GeneratedFileName,
                    RapidEnumTemplate.Generate(generationContext));
            });
    }

    private static RapidEnumGeneratorContext? CreateWithTypeContext(GeneratorAttributeSyntaxContext context)
    {
        if (context.TargetSymbol is not INamedTypeSymbol targetSymbol) return null;
        if (context.TargetNode is not ClassDeclarationSyntax classDeclarationSyntax) return null;

        if (context.Attributes
                .FirstOrDefault(x => x.AttributeClass?.Name == Constants.WithTypeAttributeName)
                ?.ConstructorArguments.FirstOrDefault().Value is not INamedTypeSymbol enumSymbol) return null;

        if (!IsInternalOrPublic(targetSymbol))
            return InvalidDeclaration(RapidEnumAnalyzer.MustBeInternalOrPublic, targetSymbol);

        if (classDeclarationSyntax.Parent is TypeDeclarationSyntax)
            return InvalidDeclaration(RapidEnumAnalyzer.MustNotBeNested, targetSymbol);

        if (!classDeclarationSyntax.Modifiers.Any(SyntaxKind.PartialKeyword))
            return InvalidDeclaration(RapidEnumAnalyzer.MustBePartial, targetSymbol);

        if (!classDeclarationSyntax.Modifiers.Any(SyntaxKind.StaticKeyword))
            return InvalidDeclaration(RapidEnumAnalyzer.MustBeStatic, targetSymbol);

        return new RapidEnumGeneratorContext(targetSymbol, enumSymbol);
    }

    private static RapidEnumGeneratorContext? CreateEnumContext(GeneratorAttributeSyntaxContext context)
    {
        if (context.TargetSymbol is not INamedTypeSymbol enumSymbol) return null;

        return IsInternalOrPublic(enumSymbol)
            ? new RapidEnumGeneratorContext(enumSymbol)
            : InvalidDeclaration(RapidEnumAnalyzer.MustBeInternalOrPublic, enumSymbol);
    }

    private static bool IsInternalOrPublic(INamedTypeSymbol symbol) =>
        symbol.DeclaredAccessibility is Accessibility.Public or Accessibility.Internal;

    private static RapidEnumGeneratorContext InvalidDeclaration(DiagnosticDescriptor descriptor, INamedTypeSymbol symbol) =>
        new(descriptor, symbol.Locations.FirstOrDefault() ?? Location.None, symbol.Name);
}
