// This file is used by Code Analysis to maintain SuppressMessage
// attributes that are applied to this project.
// Project-level suppressions either have no target or are given
// a specific target and scoped to a namespace, type, member, etc.

using System.Diagnostics.CodeAnalysis;

[assembly: SuppressMessage("Major Bug", "S3343:Caller information parameters should come at the end of the parameter list",
    Justification = "CallerMemberName always yields a value, and params must be at the end of the parameter list",
    Scope = "member", Target = "~T:Reception.App.ViewModels.MainViewModel.ShowErrorAction")]
