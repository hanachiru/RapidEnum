using System;
using System.Linq;
using System.Text;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Text;

namespace RapidEnum;

[Generator]
public class RapidEnumGenerator : IIncrementalGenerator
{
    public void Initialize(IncrementalGeneratorInitializationContext context)
    {
        context.RegisterPostInitializationOutput(static postInitializationContext =>
        {
            postInitializationContext.AddSource("RapidEnumAttribute.cs", SourceText.From($$$"""
                using System;

                namespace {{{Constants.AttributeNameSpace}}}
                {
                    [AttributeUsage(AttributeTargets.Enum)]
                    public sealed class {{{Constants.AttributeName}}} : Attribute
                    {
                        
                    }
                }
                """, Encoding.UTF8));
        });

        var provider = context.SyntaxProvider
            .ForAttributeWithMetadataName($"{Constants.AttributeNameSpace}.{Constants.AttributeName}",
                static (node, token) =>
                {
                    token.ThrowIfCancellationRequested();
                    return node is EnumDeclarationSyntax;
                },
                static (context, token) =>
                {
                    token.ThrowIfCancellationRequested();
                    if (context.TargetSymbol is not INamedTypeSymbol enumSymbol)
                    {
                        return null;
                    }

                    var generateStateMachineAttribute = context.Attributes
                        .FirstOrDefault(x => x.AttributeClass?.Name == Constants.AttributeName);
                    return generateStateMachineAttribute != null
                        ? new RapidEnumGeneratorContext(enumSymbol, generateStateMachineAttribute)
                        : null;
                })
            .Where(x => x != null);

        context.RegisterSourceOutput(provider, static (context, generationContext) =>
        {
            if (generationContext == null)
            {
                return;
            }

            var rendered = RenderStateMachine(generationContext);
            context.AddSource(generationContext.GeneratedFileName, rendered);
        });
    }

    private static string RenderStateMachine(RapidEnumGeneratorContext context)
    {
        // GetValues
        // GetNames
        // GetName
        // ToString
        // IsDefines
        // Parse
        // TryParse
        // GetEnumMemberValue();
        // GetLabel()
        return $$$"""
                  using System;
                  
                  {{{(context.NameSpace != null ?
                      $"namespace {context.NameSpace} \n{{" :
                      "")}}}
                      
                    public static class {{{context.ClassName}}}
                    {
                    
                    }
                    
                  {{{(context.NameSpace != null ? "}" : "")}}}
                  """;
    }
}