using System;
using System.Linq;
using Microsoft.CodeAnalysis;

namespace RapidEnum;

public class RapidEnumGeneratorContext
{
    public string GeneratedFileName { get; }
    public string ClassName { get; }
    
    public string? NameSpace { get; }
    public string? Accessibility { get; }
    
    public string EnumFullName { get; }
    public string[] EnumNames { get; }
    
    public RapidEnumGeneratorContext(INamedTypeSymbol enumSymbol, string? namespaceName = null, Accessibility? accessibility = null)
    {
        ClassName = $"{enumSymbol.Name}EnumExtensions";
        GeneratedFileName = $"{ClassName}.g.cs";

        Accessibility = (accessibility ?? enumSymbol.DeclaredAccessibility) switch
        {
            Microsoft.CodeAnalysis.Accessibility.Internal => "internal",
            Microsoft.CodeAnalysis.Accessibility.Public => "public",
            _ => "public"
        };

        NameSpace = namespaceName ?? (enumSymbol.ContainingNamespace.IsGlobalNamespace ? null : enumSymbol.ContainingNamespace.ToDisplayString());
        
        EnumFullName = enumSymbol.ToDisplayString(SymbolDisplayFormat.FullyQualifiedFormat);
        EnumNames = enumSymbol.GetMembers()
            .Where(x => x.Kind == SymbolKind.Field && x is IFieldSymbol { HasConstantValue: true })
            .Select(x => x.ToDisplayString())
            .ToArray();
    }   
}