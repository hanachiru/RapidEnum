using Microsoft.CodeAnalysis;

namespace RapidEnum;

public static class RapidEnumAnalyzer
{
    private const string Id = "RapidEnum";
    private const string UsageCategory = "Usage";

    public static readonly DiagnosticDescriptor MustBeInternalOrPublic = new(
        id: $"{Id}001",
        title: "The attributes provided by RapidEnum can only be used in internal or public classes",
        messageFormat: "Set the accessibility of ‘{0}’ to internal or public",
        category: UsageCategory,
        defaultSeverity: DiagnosticSeverity.Error,
        isEnabledByDefault: true
    );

    public static readonly DiagnosticDescriptor MustBePartial = new(
        id: $"{Id}002",
        title: "The attributes provided by RapidEnum can only be used in the partial class",
        messageFormat: "Give partial keyword to class ‘{0}’",
        category: UsageCategory,
        defaultSeverity: DiagnosticSeverity.Error,
        isEnabledByDefault: true
    );

    public static readonly DiagnosticDescriptor MustNotBeNested = new(
        id: $"{Id}003",
        title: "Attributes provided by RapidEnum are not available in nested classes",
        messageFormat: "Do not make class ‘{0}’ a nested class",
        category: UsageCategory,
        defaultSeverity: DiagnosticSeverity.Error,
        isEnabledByDefault: true
    );

    public static readonly DiagnosticDescriptor MustBeStatic = new(
        id: $"{Id}004",
        title: "Attributes provided by RapidEnum can only be used in static classes",
        messageFormat: "Make class ‘{0}’ static",
        category: UsageCategory,
        defaultSeverity: DiagnosticSeverity.Error,
        isEnabledByDefault: true
    );
}
