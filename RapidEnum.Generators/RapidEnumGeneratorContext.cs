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

    public RapidEnumGeneratorContext(INamedTypeSymbol enumSymbol)
    {
        ClassName = $"{enumSymbol.Name}EnumExtensions";
        GeneratedFileName = $"{ClassName}.g.cs";

        Accessibility = GetAccessibilityName(enumSymbol.DeclaredAccessibility);
        NameSpace = enumSymbol.ContainingNamespace.IsGlobalNamespace
            ? null
            : enumSymbol.ContainingNamespace.ToDisplayString();

        EnumFullName = enumSymbol.ToDisplayString(SymbolDisplayFormat.FullyQualifiedFormat);
        EnumNames = GetEnumNames(enumSymbol);
    }

    public RapidEnumGeneratorContext(INamedTypeSymbol targetSymbol, INamedTypeSymbol enumSymbol)
    {
        ClassName = targetSymbol.Name;
        GeneratedFileName = $"{ClassName}.g.cs";
        
        NameSpace = targetSymbol.ContainingNamespace.IsGlobalNamespace
            ? null
            : targetSymbol.ContainingNamespace.ToDisplayString();
        Accessibility = GetAccessibilityName(targetSymbol.DeclaredAccessibility);

        EnumFullName = enumSymbol.ToDisplayString(SymbolDisplayFormat.FullyQualifiedFormat);
        EnumNames = GetEnumNames(enumSymbol);
    }

    private static string GetAccessibilityName(Accessibility accessibility)
    {
        return accessibility switch
        {
            Microsoft.CodeAnalysis.Accessibility.Internal => "internal",
            Microsoft.CodeAnalysis.Accessibility.Public => "public",
            _ => "public"
        };
    }
    
    private static string[] GetEnumNames(INamedTypeSymbol enumSymbol)
    {
        return enumSymbol.GetMembers()
            .Where(x => x.Kind == SymbolKind.Field && x is IFieldSymbol { HasConstantValue: true })
            .Select(x => x.ToDisplayString())
            .ToArray();
    }
}