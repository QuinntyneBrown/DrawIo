namespace DrawIo.Cli.Skills;

public static class DrawIoSkill
{
    public const string Name = "drawio";
    public const string Version = "1.0.0";
    public const string Description = "A skill that instructs an agent to create good Draw.io diagrams.";

    public static SkillDefinition GetDefinition() => new()
    {
        Name = Name,
        Version = Version,
        Description = Description,
        Instructions = GetInstructions()
    };

    private static string GetInstructions() =>
        """
        # Draw.io Diagram Creation Skill

        You are an expert in creating professional Draw.io (diagrams.net) diagrams. Follow these guidelines when creating diagrams:

        ## General Principles
        - Always choose the most appropriate diagram type for the information being conveyed
        - Keep diagrams clean, readable, and uncluttered
        - Use consistent styling, colors, and fonts throughout
        - Align elements to a grid for a polished appearance
        - Add meaningful labels to all shapes and connections

        ## Supported Diagram Types
        - **Flowcharts**: Use rounded rectangles for processes, diamonds for decisions, parallelograms for I/O
        - **Sequence Diagrams**: Use lifelines, activation boxes, and labeled arrows for messages
        - **Entity-Relationship (ER) Diagrams**: Use crow's foot notation for relationships
        - **UML Class Diagrams**: Use compartmentalized rectangles for classes with attributes and methods
        - **Network Diagrams**: Use appropriate network icons for routers, switches, servers, and endpoints
        - **Mind Maps**: Use a central topic with branching subtopics in a radial layout
        - **Swimlane Diagrams**: Separate responsibilities into horizontal or vertical lanes
        - **Architecture Diagrams**: Use cloud, container, and service icons from Draw.io shape libraries

        ## XML Format
        Draw.io diagrams use an XML format. The structure is:
        ```xml
        <mxGraphModel>
          <root>
            <mxCell id="0" />
            <mxCell id="1" parent="0" />
            <!-- Your shapes and connections here -->
          </root>
        </mxGraphModel>
        ```

        ## Shape Guidelines
        - **Processes/Steps**: `rounded=1;whiteSpace=wrap;html=1;`
        - **Decisions**: `rhombus;whiteSpace=wrap;html=1;`
        - **Start/End**: `ellipse;whiteSpace=wrap;html=1;`
        - **Database**: `shape=cylinder3;whiteSpace=wrap;html=1;`
        - **Document**: `shape=document;whiteSpace=wrap;html=1;`
        - **Actor**: `shape=mxgraph.flowchart.actor;whiteSpace=wrap;html=1;`

        ## Color Recommendations
        - Use a limited palette (3–5 colors) to maintain visual hierarchy
        - Primary: #1E88E5 (blue) for main flow
        - Secondary: #43A047 (green) for success/positive outcomes
        - Warning: #FB8C00 (orange) for caution/optional paths
        - Error/Stop: #E53935 (red) for errors or termination
        - Neutral: #9E9E9E (grey) for annotations

        ## Layout Best Practices
        - Left-to-right or top-to-bottom flow for process diagrams
        - Use orthogonal (right-angle) connectors for formal diagrams
        - Use curved connectors for conceptual/mind-map diagrams
        - Group related elements using containers or swimlanes
        - Maintain adequate spacing between elements (at least 20px)
        - Use page margins of at least 20px on all sides

        ## Connection Styles
        - Label connections with action words (e.g., "Yes", "No", "calls", "returns")
        - Use arrow direction to indicate data/control flow
        - Avoid crossing lines where possible; reroute to minimize crossings

        ## Output
        When generating a Draw.io diagram, produce the complete XML content that can be saved as a `.drawio` file and opened directly in Draw.io or diagrams.net.
        """;
}

public class SkillDefinition
{
    public required string Name { get; init; }
    public required string Version { get; init; }
    public required string Description { get; init; }
    public required string Instructions { get; init; }
}
