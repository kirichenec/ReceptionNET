// This file is used by Code Analysis to maintain SuppressMessage
// attributes that are applied to this project.
// Project-level suppressions either have no target or are given
// a specific target and scoped to a namespace, type, member, etc.

using System.Diagnostics.CodeAnalysis;

// Migrations are autegenerated, so never check them
[assembly: SuppressMessage("Minor Code Smell", "S1192:String literals should not be duplicated", Justification = "<Pending>", Scope = "namespaceanddescendants", Target = "~N:Reception.Server.Auth.Migrations")]
[assembly: SuppressMessage("Style", "IDE0300:Simplify collection initialization", Justification = "<Pending>", Scope = "namespaceanddescendants", Target = "~N:Reception.Server.Auth.Migrations")]
// Ignore array parameters for tests
[assembly: SuppressMessage("Performance", "CA1861:Avoid constant arrays as arguments", Justification = "<Pending>", Scope = "namespaceanddescendants", Target = "~N:Reception.Server.Auth.Migrations")]
