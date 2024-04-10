using System;
using System.IO;
using System.Linq;
using Nuke.Common;
using Nuke.Common.Git;
using Nuke.Common.ProjectModel;
using Nuke.Common.Tools.DotNet;
using Nuke.Common.Tools.GitVersion;
using Nuke.Common.Tools.NerdbankGitVersioning;
using static Nuke.Common.IO.FileSystemTasks;

class Build : NukeBuild
{
    [NerdbankGitVersioning]
    readonly NerdbankGitVersioning NerdbankVersioning;
    
    [Solution]      readonly Solution      Solution;
    public static            int           Main() => Execute<Build>(x => x.Pack);

    [Parameter("Configuration to build - Default is 'Debug' (local) or 'Release' (server)")] readonly Configuration Configuration = IsLocalBuild ? Configuration.Debug : Configuration.Release;

    Target Clean =>
        _ => _
            .Before(Restore)
            .Executes(() =>
            {
                DotNetTasks.DotNetClean(s => s
                    .SetProject(Solution));
                EnsureCleanDirectory("artifacts");
            });

    Target Restore =>
        _ => _
            .DependsOn(Clean)
            .Executes(() =>
            {
                DotNetTasks.DotNetRestore(s => s
                    .SetProjectFile(Solution));
            });

    Target Compile =>
        _ => _
            .DependsOn(Restore)
            .Executes(() =>
            {
                DotNetTasks.DotNetBuild(s => s
                    .SetProjectFile(Solution)
                    .SetConfiguration(Configuration));
            });

    Target Pack =>
        _ => _
            .DependsOn(Compile)
            .Executes(() =>
            {
                DotNetTasks.DotNetPack(s => s
                    .SetProject(Solution)
                    .SetVersion(NerdbankVersioning.NuGetPackageVersion)
                    .SetConfiguration(Configuration)
                    .SetOutputDirectory("artifacts"));
            });
}