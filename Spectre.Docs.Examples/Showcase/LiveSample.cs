using Spectre.Console;

namespace Spectre.Docs.Examples.Showcase;

/// <summary>Demonstrates live display with scripted failure/recovery narrative.</summary>
public class LiveSample : BaseSample
{
    private const int Cluster = 0;
    private const int Backup = 1;
    private const int Mainframe = 2;
    private const int DevEnv = 3;
    private const int TestEnv = 4;
    private const int TheCloud = 5;

    private static readonly Random _random = new();

    private static readonly string[] _subsystemNames =
    [
        "Core Cluster",
        "Emergency Backup",
        "Heritage Mainframe",
        "Prototype Lab",
        "Verification Zone",
        "The Cloud™"
    ];

    /// <inheritdoc />
    public override void Run(IAnsiConsole console)
    {
        var table = CreateMonitorTable();
        var states = CreateInitialStates();
        var keyframes = ExpandNarrative(CreateNarrativeKeyframes());

        console.Live(table)
            .AutoClear(false)
            .Start(ctx =>
            {
                // Run through the narrative twice
                for (var loop = 0; loop < 2; loop++)
                {
                    foreach (var keyframe in keyframes)
                    {
                        ApplyKeyframe(states, keyframe);
                        RenderTable(table, states);
                        ctx.Refresh();
                        Thread.Sleep(keyframe.Duration);
                    }
                }
            });
    }

    private static Table CreateMonitorTable()
    {
        var table = new Table()
            .Title("[bold blue]SYSTEMS MONITORING DASHBOARD v2.3.7[/]")
            .Caption("[dim]Uptime: 4,291 days (unverified)[/]")
            .Border(TableBorder.Rounded)
            .BorderColor(Color.Blue)
            .Width(78);

        table.AddColumn(new TableColumn("[bold]Subsystem[/]").Width(18));
        table.AddColumn(new TableColumn("[bold]Status[/]").Width(14).Centered());
        table.AddColumn(new TableColumn("[bold]Load[/]").Width(14).Centered());
        table.AddColumn(new TableColumn("[bold]Message[/]").Width(20));

        return table;
    }

    private static SubsystemState[] CreateInitialStates()
    {
        return
        [
            new() { Status = "Online", Load = 45 },  // Primary Cluster
            new() { Status = "Standby", Load = 5 },  // Failover Array
            new() { Status = "Online", Load = 65 },  // Legacy Mainframe
            new() { Status = "Online", Load = 30 },  // Sector 7-G
            new() { Status = "Online", Load = 12 },  // Test Environment
            new() { Status = "Online", Load = 55 },  // The Cloud
        ];
    }

    private static List<Keyframe> CreateNarrativeKeyframes()
    {
        return
        [
            // Act 1: All is well - subtle fluctuations
            new Keyframe(600) { Changes = {
                [Cluster] = ("Online", 47, null),
                [TheCloud] = ("Online", 52, null),
            }},
            new Keyframe(500) { Changes = {
                [Mainframe] = ("Online", 68, null),
                [TestEnv] = ("Online", 15, null),
            }},
            new Keyframe(500) { Changes = {
                [Cluster] = ("Online", 44, null),
                [DevEnv] = ("Online", 32, null),
            }},

            // Act 2: Legacy Mainframe starts struggling
            new Keyframe(500) { Changes = {
                [Mainframe] = ("Online", 72, null),
                [TheCloud] = ("Online", 57, null),
            }},
            new Keyframe(500) { Changes = {
                [Mainframe] = ("Online", 78, null),
            }},
            new Keyframe(500) { Changes = {
                [Mainframe] = ("Online", 85, "Load increasing"),
                [Cluster] = ("Online", 48, null),
            }},
            new Keyframe(500) { Changes = {
                [Mainframe] = ("Online", 91, "Load increasing"),
            }},
            new Keyframe(600) { Changes = {
                [Mainframe] = ("Warning", 95, "Thermal limits"),
            }},
            new Keyframe(400) { Changes = {
                [Mainframe] = ("Warning", 98, "Thermal limits"),
                [DevEnv] = ("Online", 35, null),
            }},

            // Act 3: Cascade failure
            new Keyframe(700) { Changes = {
                [Mainframe] = ("Offline", 0, "UNRESPONSIVE"),
                [DevEnv] = ("Warning", 42, "Lost upstream"),
            }},
            new Keyframe(500) { Changes = {
                [DevEnv] = ("Warning", 48, "Lost upstream"),
                [Cluster] = ("Online", 55, null),
            }},
            new Keyframe(500) { Changes = {
                [TheCloud] = ("Warning", 68, "Rerouting..."),
                [Cluster] = ("Online", 62, "Compensating"),
            }},
            new Keyframe(400) { Changes = {
                [TheCloud] = ("Warning", 73, "Rerouting..."),
                [Cluster] = ("Online", 67, "Compensating"),
            }},

            // Act 4: Failover kicks in
            new Keyframe(600) { Changes = {
                [Backup] = ("Activating", 15, "Spinning up"),
            }},
            new Keyframe(400) { Changes = {
                [Backup] = ("Activating", 28, "Spinning up"),
                [TheCloud] = ("Warning", 75, "Rerouting..."),
            }},
            new Keyframe(500) { Changes = {
                [Backup] = ("Activating", 45, "Spinning up"),
            }},
            new Keyframe(500) { Changes = {
                [Backup] = ("Online", 58, "Taking load"),
                [DevEnv] = ("Online", 38, "Rerouted"),
            }},
            new Keyframe(400) { Changes = {
                [Backup] = ("Online", 65, "Taking load"),
                [TheCloud] = ("Online", 62, null),
            }},
            new Keyframe(500) { Changes = {
                [Mainframe] = ("Recovering", 5, "Rebooting..."),
                [TheCloud] = ("Online", 55, null),
                [Cluster] = ("Online", 58, null),
            }},

            // Act 5: Recovery
            new Keyframe(500) { Changes = {
                [Mainframe] = ("Recovering", 15, "Rebooting..."),
                [Backup] = ("Online", 62, null),
            }},
            new Keyframe(500) { Changes = {
                [Mainframe] = ("Recovering", 30, "Self-test..."),
            }},
            new Keyframe(500) { Changes = {
                [Mainframe] = ("Recovering", 45, "Self-test OK"),
                [Backup] = ("Online", 55, null),
            }},
            new Keyframe(500) { Changes = {
                [Mainframe] = ("Online", 55, "Back online"),
                [Backup] = ("Online", 48, "Shedding load"),
            }},
            new Keyframe(400) { Changes = {
                [Mainframe] = ("Online", 60, null),
                [Backup] = ("Online", 35, "Shedding load"),
                [Cluster] = ("Online", 50, null),
            }},
            new Keyframe(500) { Changes = {
                [Backup] = ("Online", 20, "Shedding load"),
            }},
            new Keyframe(500) { Changes = {
                [Backup] = ("Standby", 5, null),
                [Cluster] = ("Online", 46, null),
            }},

            // Act 6: All clear - settle back to normal
            new Keyframe(500) { Changes = {
                [Mainframe] = ("Online", 63, null),
                [TheCloud] = ("Online", 53, null),
            }},
            new Keyframe(500) { Changes = {
                [Mainframe] = ("Online", 65, null),
                [DevEnv] = ("Online", 30, null),
            }},
            new Keyframe(800), // Pause before loop
        ];
    }

    private static List<Keyframe> ExpandNarrative(List<Keyframe> keyframes, int tweenInterval = 225)
    {
        var expanded = new List<Keyframe>();
        var currentStates = CreateInitialStates();

        foreach (var keyframe in keyframes)
        {
            var tweenCount = (keyframe.Duration - 1) / tweenInterval;
            var remainingDuration = keyframe.Duration - (tweenCount * tweenInterval);

            // Generate tween frames with subtle load variations
            for (var t = 0; t < tweenCount; t++)
            {
                var tweenFrame = new Keyframe(tweenInterval);
                for (var i = 0; i < currentStates.Length; i++)
                {
                    var state = currentStates[i];
                    // Skip Offline and Standby states
                    if (state.Status is "Offline" or "Standby")
                        continue;

                    var variedLoad = VaryLoad(state.Load);
                    if (variedLoad != state.Load)
                    {
                        tweenFrame.Changes[i] = (state.Status, variedLoad, state.Message);
                        currentStates[i].Load = variedLoad;
                    }
                }
                expanded.Add(tweenFrame);
            }

            // Apply the original keyframe's changes to current state
            foreach (var (index, change) in keyframe.Changes)
            {
                currentStates[index].Status = change.Status;
                currentStates[index].Load = change.Load;
                currentStates[index].Message = change.Message;
            }

            // Add the original keyframe with remaining duration
            var finalFrame = new Keyframe(remainingDuration > 0 ? remainingDuration : tweenInterval);
            foreach (var (index, change) in keyframe.Changes)
            {
                finalFrame.Changes[index] = change;
            }
            expanded.Add(finalFrame);
        }

        return expanded;
    }

    private static int VaryLoad(int load, int variance = 3)
    {
        var delta = _random.Next(-variance, variance + 1);
        return Math.Clamp(load + delta, 0, 100);
    }

    private static void ApplyKeyframe(SubsystemState[] states, Keyframe keyframe)
    {
        foreach (var (index, change) in keyframe.Changes)
        {
            states[index].Status = change.Status;
            states[index].Load = change.Load;
            states[index].Message = change.Message;
        }
    }

    private static void RenderTable(Table table, SubsystemState[] states)
    {
        table.Rows.Clear();
        for (var i = 0; i < _subsystemNames.Length; i++)
        {
            table.AddRow(
                _subsystemNames[i],
                FormatStatus(states[i].Status),
                CreateLoadBar(states[i].Load),
                states[i].Message ?? "[dim]-[/]");
        }
    }

    private static string FormatStatus(string status) => status switch
    {
        "Online" => "[green]● Online[/]",
        "Standby" => "[dim]○ Standby[/]",
        "Warning" => "[yellow]● Warning[/]",
        "Activating" => "[cyan]◐ Activating[/]",
        "Recovering" => "[blue]● Recovering[/]",
        "Offline" => "[red]● Offline[/]",
        _ => "[grey]? Unknown[/]"
    };

    private static string CreateLoadBar(int load)
    {
        if (load == 0) return "[dim]----------[/]";
        var filled = Math.Max(1, load / 10);
        var empty = 10 - filled;
        var color = load switch
        {
            > 85 => "red",
            > 60 => "yellow",
            _ => "green"
        };
        return $"[{color}]{new string('█', filled)}[/][dim]{new string('░', empty)}[/]";
    }

    private class SubsystemState
    {
        public string Status { get; set; } = "Online";
        public int Load { get; set; }
        public string? Message { get; set; }
    }

    private class Keyframe(int duration)
    {
        public int Duration { get; } = duration;
        public Dictionary<int, (string Status, int Load, string? Message)> Changes { get; } = new();
    }
}
