using System.Collections.Generic;
using System.Linq;
using Microsoft.CodeAnalysis;

namespace RapidEnum;

public record RapidEnumGeneratorContext
{
    public RapidEnumGeneratorContext(DiagnosticDescriptor diagnosticDescriptor, Location diagnosticLocation,
        string className)
    {
        DiagnosticDescriptor = diagnosticDescriptor;
        DiagnosticLocation = diagnosticLocation;
        ClassName = className;
        NameSpace = null;
        Accessibility = null;
        EnumFullName = null;
        EnumNames = null;
        EnumMemberValues = null;
    }

    public RapidEnumGeneratorContext(INamedTypeSymbol enumSymbol)
    {
        DiagnosticDescriptor = RapidEnumAnalyzer.Default;
        DiagnosticLocation = Location.None;

        ClassName = $"{enumSymbol.Name}EnumExtensions";
        
        NameSpace = enumSymbol.ContainingNamespace.IsGlobalNamespace
            ? null
            : enumSymbol.ContainingNamespace.ToDisplayString();
        Accessibility = GetAccessibilityName(enumSymbol.DeclaredAccessibility);
        
        EnumFullName = enumSymbol.ToDisplayString(SymbolDisplayFormat.FullyQualifiedFormat);
        EnumNames = GetEnumNames(enumSymbol);
        EnumMemberValues = GetEnumMemberValues(enumSymbol);
    }

    public RapidEnumGeneratorContext(INamedTypeSymbol targetSymbol, INamedTypeSymbol enumSymbol)
    {
        DiagnosticDescriptor = RapidEnumAnalyzer.Default;
        DiagnosticLocation = Location.None;

        ClassName = targetSymbol.Name;

        NameSpace = targetSymbol.ContainingNamespace.IsGlobalNamespace
            ? null
            : targetSymbol.ContainingNamespace.ToDisplayString();
        Accessibility = GetAccessibilityName(targetSymbol.DeclaredAccessibility);

        EnumFullName = enumSymbol.ToDisplayString(SymbolDisplayFormat.FullyQualifiedFormat);
        EnumNames = GetEnumNames(enumSymbol);
        EnumMemberValues = GetEnumMemberValues(enumSymbol);
    }

    public string GeneratedFileName => $"{ClassName}.g.cs";

    public DiagnosticDescriptor DiagnosticDescriptor { get; }

    public Location DiagnosticLocation { get; }

    public string ClassName { get; }

    public string? NameSpace { get; }
    public string? Accessibility { get; }

    public string? EnumFullName { get; }
    public string[]? EnumNames { get; }
    public string?[]? EnumMemberValues { get; }

    public virtual bool Equals(RapidEnumGeneratorContext? other)
    {
        if (ReferenceEquals(null, other)) return false;
        if (ReferenceEquals(this, other)) return true;

        return EqualityContract == other.EqualityContract &&
               DiagnosticDescriptor.Equals(other.DiagnosticDescriptor) &&
               EqualityComparer<Location>.Default.Equals(DiagnosticLocation, other.DiagnosticLocation) &&
               EqualityComparer<string>.Default.Equals(ClassName, other.ClassName) &&
               EqualityComparer<string?>.Default.Equals(NameSpace, other.NameSpace) &&
               EqualityComparer<string?>.Default.Equals(Accessibility, other.Accessibility) &&
               EqualityComparer<string?>.Default.Equals(EnumFullName, other.EnumFullName) &&
               (EnumNames is null ? other.EnumNames is null : other.EnumNames is not null && EnumNames.SequenceEqual(other.EnumNames)) &&
               (EnumMemberValues is null ? other.EnumMemberValues is null : other.EnumMemberValues is not null && EnumMemberValues.SequenceEqual(other.EnumMemberValues));
    }

    private static string GetAccessibilityName(Accessibility accessibility)
    {
        return accessibility switch
        {
            Microsoft.CodeAnalysis.Accessibility.Internal => "internal",
            Microsoft.CodeAnalysis.Accessibility.Public => "public",
            _ => ""
        };
    }

    private static string[] GetEnumNames(INamedTypeSymbol enumSymbol)
    {
        return enumSymbol.GetMembers()
            .Where(x => x.Kind == SymbolKind.Field && x is IFieldSymbol { HasConstantValue: true })
            .Select(x => x.ToDisplayString())
            .ToArray();
    }

    private static string?[] GetEnumMemberValues(INamedTypeSymbol enumSymbol)
    {
        return enumSymbol.GetMembers()
            .Select(x =>
            {
                return x.GetAttributes()
                    .Where(static x =>
                        x.AttributeClass?.Name == nameof(System.Runtime.Serialization.EnumMemberAttribute))
                    .Select(static x => x.NamedArguments.FirstOrDefault().Value.Value?.ToString())
                    .FirstOrDefault();
            })
            .ToArray();
    }
    
    public override int GetHashCode()
    {
        var hashCode = ClassName.GetHashCode();
        hashCode = (hashCode * 397) ^ DiagnosticDescriptor.GetHashCode();
        hashCode = (hashCode * 397) ^ DiagnosticLocation.GetHashCode();
        hashCode = (hashCode * 397) ^ (NameSpace?.GetHashCode() ?? 17);
        hashCode = (hashCode * 397) ^ (Accessibility?.GetHashCode() ?? 17);
        hashCode = (hashCode * 397) ^ (EnumFullName?.GetHashCode() ?? 17);
        hashCode = (hashCode * 397) ^ (EnumNames?.GetHashCode() ?? 17);
        hashCode = (hashCode * 397) ^ (EnumMemberValues?.GetHashCode() ?? 17);
        return hashCode;
    }
}