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
        {
            var source = RapidEnumTemplate.GenerateAttributes();
            initContext.AddSource("RapidEnumAttributes.g.cs", source);
        });

        var classProvider = context.SyntaxProvider
            .ForAttributeWithMetadataName($"{Constants.AttributeNameSpace}.{Constants.MarkerAttributeName}",
                static (node, token) =>
                {
                    token.ThrowIfCancellationRequested();
                    return node is ClassDeclarationSyntax;
                },
                static (context, token) =>
                {
                    token.ThrowIfCancellationRequested();
                    if (context.TargetSymbol is not INamedTypeSymbol targetSymbol) return null;
                    if (context.TargetNode is not ClassDeclarationSyntax classDeclarationSyntax) return null;

                    var enumSymbol = context.Attributes
                        .FirstOrDefault(x => x?.AttributeClass?.Name == Constants.MarkerAttributeName)
                        ?.ConstructorArguments.FirstOrDefault().Value as INamedTypeSymbol;

                    if (enumSymbol == null) return null;

                    var accessibility = context.TargetSymbol.DeclaredAccessibility;
                    if (accessibility != Accessibility.Public && accessibility != Accessibility.Internal)
                        return new RapidEnumGeneratorContext(RapidEnumAnalyzer.MustBeInternalOrPublic,
                            targetSymbol.Locations.FirstOrDefault() ?? Location.None, targetSymbol.Name);

                    if (classDeclarationSyntax.Parent is TypeDeclarationSyntax)
                        return new RapidEnumGeneratorContext(
                            RapidEnumAnalyzer.MustNotBeNested, targetSymbol.Locations.FirstOrDefault() ?? Location.None,
                            targetSymbol.Name);

                    if (classDeclarationSyntax.Modifiers.Any(SyntaxKind.PartialKeyword) == false)
                        return new RapidEnumGeneratorContext(
                            RapidEnumAnalyzer.MustBePartial, targetSymbol.Locations.FirstOrDefault() ?? Location.None,
                            targetSymbol.Name);

                    if (classDeclarationSyntax.Modifiers.Any(SyntaxKind.StaticKeyword) == false)
                        return new RapidEnumGeneratorContext(
                            RapidEnumAnalyzer.MustBeStatic, targetSymbol.Locations.FirstOrDefault() ?? Location.None,
                            targetSymbol.Name);

                    return new RapidEnumGeneratorContext(targetSymbol, enumSymbol);
                }).Where(x => x != null);

        context.RegisterSourceOutput(classProvider, static (context, generationContext) =>
        {
            if (generationContext == null) return;

            if (!generationContext.DiagnosticDescriptor.Equals(RapidEnumAnalyzer.Default))
            {
                context.ReportDiagnostic(Diagnostic.Create(generationContext.DiagnosticDescriptor,
                    generationContext.DiagnosticLocation, generationContext.ClassName));
                return;
            }

            var rendered = RapidEnumTemplate.Generate(generationContext);
            context.AddSource(generationContext.GeneratedFileName, rendered);
        });

        var enumProvider = context.SyntaxProvider
            .ForAttributeWithMetadataName($"{Constants.AttributeNameSpace}.{Constants.AttributeName}",
                static (node, token) =>
                {
                    token.ThrowIfCancellationRequested();
                    return node is EnumDeclarationSyntax;
                },
                static (context, token) =>
                {
                    token.ThrowIfCancellationRequested();
                    if (context.TargetSymbol is not INamedTypeSymbol enumSymbol) return null;

                    var generateStateMachineAttribute = context.Attributes
                        .FirstOrDefault(x => x.AttributeClass?.Name == Constants.AttributeName);

                    if (generateStateMachineAttribute == null) return null;

                    var accessibility = enumSymbol.DeclaredAccessibility;
                    if (accessibility != Accessibility.Public && accessibility != Accessibility.Internal)
                        return new RapidEnumGeneratorContext(RapidEnumAnalyzer.MustBeInternalOrPublic,
                            enumSymbol.Locations.FirstOrDefault() ?? Location.None, enumSymbol.Name);

                    return new RapidEnumGeneratorContext(enumSymbol);
                })
            .Where(x => x != null);

        context.RegisterSourceOutput(enumProvider, static (context, generationContext) =>
        {
            if (generationContext == null) return;

            if (!generationContext.DiagnosticDescriptor.Equals(RapidEnumAnalyzer.Default))
            {
                context.ReportDiagnostic(Diagnostic.Create(generationContext.DiagnosticDescriptor,
                    generationContext.DiagnosticLocation, generationContext.ClassName));
                return;
            }

            var rendered = RapidEnumTemplate.Generate(generationContext);
            context.AddSource(generationContext.GeneratedFileName, rendered);
        });
    }
}