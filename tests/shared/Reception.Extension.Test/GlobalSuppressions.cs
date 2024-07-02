// This file is used by Code Analysis to maintain SuppressMessage
// attributes that are applied to this project.
// Project-level suppressions either have no target or are given
// a specific target and scoped to a namespace, type, member, etc.

using System.Diagnostics.CodeAnalysis;

// Ignore local functions for tests
[assembly: SuppressMessage("Style", "IDE0039:Use local function", Justification = "<Pending>", Scope = "namespaceanddescendants", Target = "~N:Reception.Extension.Test")]
[assembly: SuppressMessage("Style", "IDE0039:Use local function", Justification = "<Pending>", Scope = "namespaceanddescendants", Target = "~N:Reception.Extension.Converters.Test")]
// Ignore array parameters for tests
[assembly: SuppressMessage("Performance", "CA1861:Avoid constant arrays as arguments", Justification = "<Pending>", Scope = "namespaceanddescendants", Target = "~N:Reception.Extension.Test")]
[assembly: SuppressMessage("Performance", "CA1861:Avoid constant arrays as arguments", Justification = "<Pending>", Scope = "namespaceanddescendants", Target = "~N:Reception.Extension.Converters.Test")]
// Bug still not fixed
// https://github.com/xunit/xunit/issues/2075
[assembly: SuppressMessage("Usage", "xUnit1010:The value is not convertible to the method parameter type", Justification = "<Pending>", Scope = "namespaceanddescendants", Target = "~N:Reception.Extension.Test")]
// Ignore ISerializable
[assembly: SuppressMessage("Usage", "xUnit1042:The member referenced by the MemberData attribute returns untyped data rows", Justification = "<Pending>", Scope = "type", Target = "~T:Reception.Extension.Converters.Test.NewtonsoftConvertersTests")]
[assembly: SuppressMessage("Usage", "xUnit1045:Avoid using TheoryData type arguments that might not be serializable", Justification = "<Pending>", Scope = "type", Target = "~T:Reception.Extension.Converters.Test.NewtonsoftConvertersTests")]
