using System.Linq;
using Microsoft.CodeAnalysis;

namespace RapidEnum;

public sealed record RapidEnumGeneratorContext
{
    /// <summary>Creates a context that only reports a diagnostic; no source is emitted for it.</summary>
    public RapidEnumGeneratorContext(
        DiagnosticDescriptor diagnosticDescriptor,
        Location diagnosticLocation,
        string className)
    {
        DiagnosticDescriptor = diagnosticDescriptor;
        DiagnosticLocation = diagnosticLocation;
        ClassName = className;
    }

    /// <summary>Creates a context for an enum marked with <c>[RapidEnum]</c>.</summary>
    public RapidEnumGeneratorContext(INamedTypeSymbol enumSymbol)
        : this(enumSymbol, enumSymbol, $"{enumSymbol.Name}EnumExtensions")
    {
    }

    /// <summary>Creates a context for a class marked with <c>[RapidEnumWithType(typeof(...))]</c>.</summary>
    public RapidEnumGeneratorContext(INamedTypeSymbol targetSymbol, INamedTypeSymbol enumSymbol)
        : this(targetSymbol, enumSymbol, targetSymbol.Name)
    {
    }

    private RapidEnumGeneratorContext(INamedTypeSymbol declaringSymbol, INamedTypeSymbol enumSymbol, string className)
    {
        ClassName = className;

        NameSpace = declaringSymbol.ContainingNamespace.IsGlobalNamespace
            ? null
            : declaringSymbol.ContainingNamespace.ToDisplayString();
        Accessibility = GetAccessibilityName(declaringSymbol.DeclaredAccessibility);

        EnumFullName = enumSymbol.ToDisplayString(SymbolDisplayFormat.FullyQualifiedFormat);
        EnumMembers = GetEnumMembers(enumSymbol);
    }

    public string GeneratedFileName => $"{ClassName}.g.cs";

    /// <summary>Non-null when the declaration is invalid; in that case no source is generated.</summary>
    public DiagnosticDescriptor? DiagnosticDescriptor { get; }

    public Location DiagnosticLocation { get; } = Location.None;

    public string ClassName { get; }

    public string? NameSpace { get; }
    public string? Accessibility { get; }

    public string? EnumFullName { get; }
    public EnumMemberInfo[]? EnumMembers { get; }

    public bool Equals(RapidEnumGeneratorContext? other)
    {
        if (other is null) return false;
        if (ReferenceEquals(this, other)) return true;

        return Equals(DiagnosticDescriptor, other.DiagnosticDescriptor) &&
               DiagnosticLocation.Equals(other.DiagnosticLocation) &&
               ClassName == other.ClassName &&
               NameSpace == other.NameSpace &&
               Accessibility == other.Accessibility &&
               EnumFullName == other.EnumFullName &&
               SequenceEquals(EnumMembers, other.EnumMembers);
    }

    public override int GetHashCode()
    {
        var hashCode = ClassName.GetHashCode();
        hashCode = (hashCode * 397) ^ (DiagnosticDescriptor?.GetHashCode() ?? 0);
        hashCode = (hashCode * 397) ^ DiagnosticLocation.GetHashCode();
        hashCode = (hashCode * 397) ^ (NameSpace?.GetHashCode() ?? 0);
        hashCode = (hashCode * 397) ^ (Accessibility?.GetHashCode() ?? 0);
        hashCode = (hashCode * 397) ^ (EnumFullName?.GetHashCode() ?? 0);
        hashCode = (hashCode * 397) ^ SequenceHashCode(EnumMembers);
        return hashCode;
    }

    private static bool SequenceEquals<T>(T[]? left, T[]? right)
    {
        if (ReferenceEquals(left, right)) return true;
        if (left is null || right is null) return false;
        return left.SequenceEqual(right);
    }

    // Hash the elements, not the array reference, so that structurally equal contexts agree on their hash code.
    private static int SequenceHashCode<T>(T[]? values)
    {
        if (values is null) return 0;

        var hashCode = values.Length;
        foreach (var value in values)
        {
            hashCode = (hashCode * 397) ^ (value?.GetHashCode() ?? 0);
        }

        return hashCode;
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

    private static EnumMemberInfo[] GetEnumMembers(INamedTypeSymbol enumSymbol)
    {
        return enumSymbol.GetMembers()
            .OfType<IFieldSymbol>()
            .Where(static x => x.HasConstantValue)
            .Select(static x => new EnumMemberInfo(x.ToDisplayString(), GetEnumMemberValue(x)))
            .ToArray();
    }

    private static string? GetEnumMemberValue(IFieldSymbol field)
    {
        return field.GetAttributes()
            .Where(static x =>
                x.AttributeClass?.Name == nameof(System.Runtime.Serialization.EnumMemberAttribute))
            .Select(static x => x.NamedArguments
                .FirstOrDefault(static arg => arg.Key == nameof(System.Runtime.Serialization.EnumMemberAttribute.Value))
                .Value.Value?.ToString())
            .FirstOrDefault();
    }
}
