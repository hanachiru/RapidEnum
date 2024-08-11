using Microsoft.CodeAnalysis;

namespace RapidEnum;

public class RapidEnumGeneratorContext
{
    public string GeneratedFileName { get; }
    public string ClassName { get; }
    public string EnumFullName { get; }
    public string? NameSpace { get; }
    
    public RapidEnumGeneratorContext(INamedTypeSymbol enumSymbol, AttributeData attributeData)
    {
        NameSpace = enumSymbol.ContainingNamespace.ToDisplayString();
        ClassName = $"{enumSymbol.ToDisplayString().Replace(".", "_")}ExtensionForRapidEnum";
        EnumFullName = enumSymbol.ToDisplayString(SymbolDisplayFormat.FullyQualifiedFormat);
        GeneratedFileName = $"{ClassName}.cs";
    }   
}