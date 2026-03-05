namespace DrawIo.Cli.Skills;

public static class DrawIoSkill
{
    public const string Name = "drawio";
    public const string Version = "2.0.0";
    public const string Description = "A skill that instructs an agent to create professional-quality Draw.io diagrams matching official example standards.";

    public static SkillDefinition GetDefinition() => new()
    {
        Name = Name,
        Version = Version,
        Description = Description,
        Instructions = GetInstructions()
    };

    private static string GetInstructions() =>
        """
        # Draw.io Diagram Creation Skill v2.0

        You are an expert in creating professional Draw.io (diagrams.net) diagrams. Every diagram you produce must match the quality standards of the official draw.io example gallery. Follow these guidelines precisely.

        ## 1. XML Document Structure

        Every .drawio file uses this exact structure:

        ```xml
        <mxfile host="app.diagrams.net" modified="2024-01-01T00:00:00.000Z" agent="DrawIo Skill" version="24.0.0" type="device">
          <diagram name="Page-1" id="page1">
            <mxGraphModel dx="1422" dy="794" grid="1" gridSize="10" guides="1" tooltips="1" connect="1" arrows="1" fold="1" page="1" pageScale="1" pageWidth="1169" pageHeight="827" math="0" shadow="0">
              <root>
                <mxCell id="0" />
                <mxCell id="1" parent="0" />
                <!-- All shapes and edges go here with parent="1" or parent="containerId" -->
              </root>
            </mxGraphModel>
          </diagram>
        </mxfile>
        ```

        **Critical rules:**
        - Cell id="0" is the absolute root. Cell id="1" is the default layer with parent="0".
        - All visible shapes use `vertex="1"`. All connectors use `edge="1"`.
        - Parent-child nesting is expressed via the `parent` attribute, NOT via XML nesting.
        - Child geometry coordinates are relative to the parent container.
        - Use sequential numeric IDs starting from 2 (e.g., "2", "3", "4"...).
        - Multi-page diagrams: add additional `<diagram name="Page-2" id="page2">` elements inside `<mxfile>`.

        ## 2. Official Color Palette

        Use ONLY these official draw.io fill/stroke color pairs. They are designed for visual harmony and accessibility:

        | Role           | fillColor  | strokeColor | Use For                                    |
        |----------------|------------|-------------|--------------------------------------------|
        | Blue           | `#dae8fc`  | `#6c8ebf`   | Primary flow, main processes, default      |
        | Green          | `#d5e8d4`  | `#82b366`   | Success, completion, positive outcomes     |
        | Yellow         | `#fff2cc`  | `#d6b656`   | Decisions, warnings, conditional paths     |
        | Orange         | `#ffe6cc`  | `#d79b00`   | Caution, optional, secondary flow          |
        | Red/Pink       | `#f8cecc`  | `#b85450`   | Errors, stop, critical, termination        |
        | Purple         | `#e1d5e7`  | `#9673a6`   | External systems, actors, third-party      |
        | Light Blue     | `#b1ddf0`  | `#10739e`   | Data stores, databases, infrastructure     |
        | White          | `#ffffff`  | `#000000`   | Neutral, annotations, labels               |
        | Dark/Black     | `#000000`  | `#000000`   | Start/end terminators only                 |

        ### Named Themes

        **Corporate Blue Theme:** Primary=#dae8fc/#6c8ebf, Accent=#b1ddf0/#10739e, Success=#d5e8d4/#82b366, Warning=#fff2cc/#d6b656, Error=#f8cecc/#b85450
        **Warm Earth Theme:** Primary=#ffe6cc/#d79b00, Accent=#fff2cc/#d6b656, Success=#d5e8d4/#82b366, Neutral=#f5f5f5/#666666, Error=#f8cecc/#b85450
        **Cool Contrast Theme:** Primary=#e1d5e7/#9673a6, Accent=#dae8fc/#6c8ebf, Success=#d5e8d4/#82b366, Warning=#ffe6cc/#d79b00, Error=#f8cecc/#b85450

        **Rules:**
        - Use 3-5 colors maximum per diagram.
        - Assign colors by semantic meaning, not decoration.
        - Swimlane lanes each get a distinct color from the palette.
        - Keep all text readable: dark text (#333333) on light fills.

        ## 3. Typography

        | Level       | fontSize | fontStyle | fontFamily        | Use For                        |
        |-------------|----------|-----------|-------------------|--------------------------------|
        | Title       | 18-24    | 1 (bold)  | Helvetica         | Diagram title, page headers    |
        | Heading     | 14-16    | 1 (bold)  | Helvetica         | Container/swimlane headers     |
        | Body        | 11-12    | 0         | Helvetica         | Shape labels, descriptions     |
        | Annotation  | 9-10     | 2 (italic)| Helvetica         | Notes, edge labels, comments   |
        | Code/Mono   | 10-11    | 0         | Courier New       | Code, technical identifiers    |

        fontStyle values: 0=normal, 1=bold, 2=italic, 3=bold+italic

        ## 4. Shape Styles Reference

        ### 4.1 Flowchart Shapes

        **Process/Action (rounded rectangle):**
        ```
        rounded=1;whiteSpace=wrap;html=1;absoluteArcSize=1;arcSize=10;fillColor=#dae8fc;strokeColor=#6c8ebf;
        ```
        Geometry: width="120" height="40"

        **Decision (diamond):**
        ```
        rhombus;whiteSpace=wrap;html=1;fillColor=#fff2cc;strokeColor=#d6b656;
        ```
        Geometry: width="80" height="60"

        **Start (filled circle):**
        ```
        ellipse;fillColor=#000000;strokeColor=#000000;
        ```
        Geometry: width="30" height="30"

        **End (bullseye):**
        ```
        ellipse;html=1;shape=endState;fillColor=#000000;strokeColor=#000000;
        ```
        Geometry: width="30" height="30"

        **I/O (parallelogram):**
        ```
        shape=parallelogram;perimeter=parallelogramPerimeter;whiteSpace=wrap;html=1;fixedSize=1;fillColor=#e1d5e7;strokeColor=#9673a6;
        ```
        Geometry: width="120" height="40"

        **Document:**
        ```
        shape=document;whiteSpace=wrap;html=1;boundedLbl=1;backgroundOutline=1;size=0.27;fillColor=#dae8fc;strokeColor=#6c8ebf;
        ```
        Geometry: width="120" height="60"

        **Database (cylinder):**
        ```
        shape=cylinder3;whiteSpace=wrap;html=1;boundedLbl=1;backgroundOutline=1;size=15;fillColor=#b1ddf0;strokeColor=#10739e;
        ```
        Geometry: width="80" height="80"

        **Manual operation (trapezoid):**
        ```
        shape=trapezoid;perimeter=trapezoidPerimeter;whiteSpace=wrap;html=1;fixedSize=1;fillColor=#ffe6cc;strokeColor=#d79b00;
        ```
        Geometry: width="120" height="40"

        **Subprocess (double-bordered rectangle):**
        ```
        rounded=1;whiteSpace=wrap;html=1;arcSize=10;fillColor=#dae8fc;strokeColor=#6c8ebf;
        ```
        With child indicator: add shape=mxgraph.flowchart.predefined_process if needed.

        **Fork/Join bar:**
        ```
        html=1;points=[];perimeter=orthogonalPerimeter;fillColor=#000000;strokeColor=#000000;
        ```
        Geometry: width="200" height="5" (horizontal) or width="5" height="80" (vertical)

        ### 4.2 UML Class Diagram Shapes

        **Class box (with compartments):**
        ```
        swimlane;fontStyle=1;align=center;verticalAlign=top;childLayout=stackLayout;horizontal=1;startSize=26;horizontalStack=0;resizeParent=1;resizeLast=0;collapsible=1;marginBottom=0;rounded=0;shadow=0;strokeWidth=1;fillColor=#dae8fc;strokeColor=#6c8ebf;
        ```
        Geometry: width="160" height varies (startSize=26 for header + 26 per row + 8 for separator)

        **Attribute/Method row:**
        ```
        text;align=left;verticalAlign=top;spacingLeft=4;spacingRight=4;overflow=hidden;rotatable=0;points=[[0,0.5],[1,0.5]];portConstraint=eastwest;
        ```
        Geometry: y increments by 26, width matches parent, height="26"

        **Separator line:**
        ```
        line;html=1;strokeWidth=1;align=left;verticalAlign=middle;spacingTop=-1;spacingLeft=3;spacingRight=3;rotatable=0;labelPosition=right;points=[];portConstraint=eastwest;
        ```
        Geometry: width matches parent, height="8"

        **Interface (with stereotype):**
        Use same class style but set value to `&lt;&lt;interface&gt;&gt;&lt;br&gt;InterfaceName` and fontStyle=3

        **Abstract class:**
        Use same class style but set fontStyle=2 (italic) for the class name.

        ### 4.3 Sequence Diagram Shapes

        **Lifeline (actor header + dashed line):**
        ```
        shape=umlLifeline;perimeter=lifelinePerimeter;whiteSpace=wrap;html=1;container=1;dropTarget=0;collapsible=0;recursiveResize=0;outlineConnect=0;portConstraint=eastwest;newEdgeStyle={"edgeStyle":"elbowEdgeStyle","elbow":"vertical","curved":0,"rounded":0};fillColor=#dae8fc;strokeColor=#6c8ebf;
        ```
        Geometry: width="100" height="300" (tall to accommodate messages)

        **Activation box:**
        ```
        html=1;points=[];perimeter=orthogonalPerimeter;outlineConnect=0;targetShapes=umlLifeline;portConstraint=eastwest;newEdgeStyle={"edgeStyle":"elbowEdgeStyle","elbow":"vertical","curved":0,"rounded":0};fillColor=#dae8fc;strokeColor=#6c8ebf;
        ```
        Geometry: width="10" height varies, centered on lifeline

        **Synchronous message:**
        ```
        html=1;verticalAlign=bottom;endArrow=block;curved=0;rounded=0;endFill=1;
        ```

        **Asynchronous message:**
        ```
        html=1;verticalAlign=bottom;endArrow=open;curved=0;rounded=0;endFill=0;dashed=1;
        ```

        **Return message:**
        ```
        html=1;verticalAlign=bottom;endArrow=open;curved=0;rounded=0;dashed=1;endFill=0;
        ```

        ### 4.4 Entity-Relationship Shapes

        **Entity box:**
        Use same swimlane/stackLayout pattern as UML class, but with ER-specific content.

        **Relationships with crow's foot notation:**
        - One-to-many: `endArrow=ERmany;startArrow=ERone;endFill=0;startFill=0;`
        - Many-to-many: `endArrow=ERmany;startArrow=ERmany;endFill=0;startFill=0;`
        - One-to-one: `endArrow=ERone;startArrow=ERone;endFill=0;startFill=0;`
        - Zero-or-one: `endArrow=ERzeroToOne;startArrow=ERone;endFill=0;startFill=0;`
        - One-or-many: `endArrow=ERoneToMany;startArrow=ERone;endFill=0;startFill=0;`
        - Zero-or-many: `endArrow=ERzeroToMany;startArrow=ERone;endFill=0;startFill=0;`

        ### 4.5 Network and Architecture Shapes

        **Cloud:**
        ```
        ellipse;shape=cloud;whiteSpace=wrap;html=1;fillColor=#dae8fc;strokeColor=#6c8ebf;
        ```
        Geometry: width="120" height="80"

        **Server:**
        ```
        shape=mxgraph.cisco.servers.standard_server;sketch=0;fillColor=#036897;strokeColor=#ffffff;
        ```

        **Container/boundary group:**
        ```
        rounded=1;whiteSpace=wrap;html=1;arcSize=5;dashed=1;dashPattern=5 2;fillColor=none;strokeColor=#6c8ebf;verticalAlign=top;fontStyle=1;fontSize=14;spacingTop=5;
        ```

        ### 4.6 Swimlane Shapes

        **Outer swimlane container:**
        ```
        swimlane;html=1;childLayout=stackLayout;resizeParent=1;resizeParentMax=0;horizontal=0;startSize=20;horizontalStack=0;
        ```

        **Individual lane (horizontal lane with vertical header):**
        ```
        swimlane;html=1;startSize=20;horizontal=0;fillColor=#dae8fc;strokeColor=#6c8ebf;
        ```
        Each lane gets a distinct fill color from the palette.

        ### 4.7 Mind Map Shapes

        **Central topic:**
        ```
        ellipse;whiteSpace=wrap;html=1;fillColor=#dae8fc;strokeColor=#6c8ebf;fontSize=16;fontStyle=1;
        ```
        Geometry: width="160" height="80"

        **Branch topic:**
        ```
        rounded=1;whiteSpace=wrap;html=1;arcSize=50;fillColor=#d5e8d4;strokeColor=#82b366;
        ```
        Geometry: width="120" height="40"

        **Leaf topic:**
        ```
        rounded=1;whiteSpace=wrap;html=1;arcSize=50;fillColor=#fff2cc;strokeColor=#d6b656;fontSize=10;
        ```
        Geometry: width="100" height="30"

        **Mind map connectors:**
        ```
        edgeStyle=entityRelationEdgeStyle;curved=1;rounded=0;html=1;endArrow=none;endFill=0;strokeWidth=2;
        ```

        ### 4.8 Org Chart Shapes

        **Person card:**
        ```
        rounded=1;whiteSpace=wrap;html=1;arcSize=10;fillColor=#dae8fc;strokeColor=#6c8ebf;shadow=1;
        ```
        Geometry: width="160" height="60"

        **Department header:**
        ```
        rounded=1;whiteSpace=wrap;html=1;arcSize=10;fillColor=#6c8ebf;strokeColor=#6c8ebf;fontColor=#ffffff;fontStyle=1;fontSize=14;
        ```
        Geometry: width="180" height="40"

        **Org chart connectors (orthogonal with waypoints):**
        ```
        edgeStyle=orthogonalEdgeStyle;rounded=1;orthogonalLoop=1;jettySize=auto;html=1;endArrow=none;endFill=0;strokeWidth=1;strokeColor=#999999;
        ```

        ### 4.9 C4 Model Shapes

        **Person:**
        ```
        html=1;fontSize=11;dashed=0;whiteSpace=wrap;fillColor=#083F75;strokeColor=#06315C;fontColor=#ffffff;shape=mxgraph.c4.person2;align=center;metaEdit=1;points=[[0.5,0,0],[1,0.5,0],[1,0.75,0],[0.75,1,0],[0.5,1,0],[0.25,1,0],[0,0.75,0],[0,0.5,0]];resizable=0;
        ```

        **Software System:**
        ```
        rounded=1;whiteSpace=wrap;html=1;labelBackgroundColor=none;fillColor=#1061B0;fontColor=#ffffff;align=center;arcSize=10;strokeColor=#0D5091;metaEdit=1;resizable=0;
        ```

        **Container:**
        ```
        rounded=1;whiteSpace=wrap;html=1;labelBackgroundColor=none;fillColor=#23A2D9;fontColor=#ffffff;align=center;arcSize=10;strokeColor=#0E7DAD;metaEdit=1;resizable=0;
        ```

        **System boundary:**
        ```
        rounded=1;fontSize=11;whiteSpace=wrap;html=1;dashed=1;arcSize=20;fillColor=none;strokeColor=#666666;fontColor=#333333;labelBackgroundColor=none;align=left;verticalAlign=bottom;labelBorderColor=none;spacingTop=0;spacing=10;dashPattern=8 4;metaEdit=1;rotatable=0;perimeter=rectanglePerimeter;noLabel=0;labelPadding=0;allowArrows=0;connectable=0;expand=0;recursiveResize=0;editable=1;pointerEvents=0;absoluteArcSize=1;points=[[0.25,0,0],[0.5,0,0],[0.75,0,0],[1,0.25,0],[1,0.5,0],[1,0.75,0],[0.75,1,0],[0.5,1,0],[0.25,1,0],[0,0.75,0],[0,0.5,0],[0,0.25,0]];
        ```

        ## 5. Connector Styles Reference

        ### 5.1 Routing Styles

        **Orthogonal (right-angle, formal):**
        ```
        edgeStyle=orthogonalEdgeStyle;rounded=0;orthogonalLoop=1;jettySize=auto;html=1;
        ```

        **Elbow (L-shaped):**
        ```
        edgeStyle=elbowEdgeStyle;elbow=vertical;rounded=0;html=1;
        ```

        **Curved (informal, mind maps):**
        ```
        edgeStyle=entityRelationEdgeStyle;curved=1;rounded=0;html=1;
        ```

        **Straight (direct connection):**
        ```
        rounded=0;orthogonalLoop=1;jettySize=auto;html=1;
        ```

        ### 5.2 Arrowhead Catalog

        | Type               | Style                                           | Use Case                       |
        |--------------------|--------------------------------------------------|-------------------------------|
        | Filled arrow       | `endArrow=classic;endFill=1`                     | Default flow direction         |
        | Open arrow         | `endArrow=open;endFill=0`                        | Association, data flow         |
        | Filled open        | `endArrow=open;endFill=1`                        | Navigable association          |
        | Hollow triangle    | `endArrow=block;endFill=0;endSize=10`            | UML inheritance/generalization |
        | Filled triangle    | `endArrow=block;endFill=1;endSize=10`            | UML realization                |
        | Hollow diamond     | `endArrow=diamondThin;endFill=0;endSize=16`      | UML aggregation                |
        | Filled diamond     | `endArrow=diamondThin;endFill=1;endSize=16`      | UML composition                |
        | No arrow           | `endArrow=none;endFill=0`                        | Undirected association         |
        | Half circle        | `endArrow=halfCircle;endFill=0;endSize=6`        | Provided interface (lollipop)  |
        | Oval               | `endArrow=oval;endFill=0;endSize=10`             | Required interface (socket)    |
        | Crow's foot many   | `endArrow=ERmany;endFill=0`                      | ER one-to-many                 |
        | Crow's foot one    | `endArrow=ERone;endFill=0`                       | ER one-to-one                  |

        ### 5.3 Line Styles

        | Type          | Style                                    |
        |---------------|------------------------------------------|
        | Solid         | (default, no extra attribute)            |
        | Dashed        | `dashed=1;`                              |
        | Dotted        | `dashed=1;dashPattern=1 4;`              |
        | Thick         | `strokeWidth=2;`                         |
        | Highlighted   | `strokeWidth=3;strokeColor=#E53935;`     |

        ### 5.4 Edge Labels

        ```xml
        <mxCell value="label text" style="edgeLabel;html=1;align=center;verticalAlign=middle;resizable=0;points=[];" parent="edgeId" vertex="1" connectable="0">
          <mxGeometry x="0.5" relative="1" as="geometry">
            <mxPoint as="offset" />
          </mxGeometry>
        </mxCell>
        ```
        - `x` relative: 0=source end, 0.5=midpoint, 1=target end, -1=before source
        - Use `<mxPoint>` offset for fine pixel adjustment.

        ### 5.5 Multiplicity Labels (UML/ER)

        ```xml
        <!-- Near source -->
        <mxCell value="1" style="resizable=0;align=left;verticalAlign=bottom;labelBackgroundColor=none;fontSize=12;" parent="edgeId" connectable="0" vertex="1">
          <mxGeometry x="-1" relative="1" as="geometry" />
        </mxCell>
        <!-- Near target -->
        <mxCell value="*" style="resizable=0;align=right;verticalAlign=bottom;labelBackgroundColor=none;fontSize=12;" parent="edgeId" connectable="0" vertex="1">
          <mxGeometry x="1" relative="1" as="geometry" />
        </mxCell>
        ```

        ## 6. Layout and Composition Rules

        ### 6.1 Spacing Standards

        | Element                    | Minimum Spacing |
        |----------------------------|-----------------|
        | Page margin (all sides)    | 40px            |
        | Between shapes (same level)| 40px horizontal, 50px vertical |
        | Between swimlanes          | 0px (they share borders)       |
        | Inside container padding   | 20px            |
        | Between connected shapes   | 60-80px (to allow edge labels) |
        | Legend from main diagram   | 40px            |

        ### 6.2 Flow Direction

        - **Flowcharts:** Top-to-bottom (preferred) or left-to-right
        - **Sequence diagrams:** Top-to-bottom (time flows down)
        - **Org charts:** Top-to-bottom (hierarchy flows down)
        - **Network diagrams:** Top-to-bottom (cloud/internet at top, devices at bottom)
        - **Mind maps:** Center-outward (radial)
        - **Timelines:** Left-to-right (chronological)
        - **Data flow:** Left-to-right

        ### 6.3 Grouping and Containment

        Use containers to group related elements:
        - **Swimlanes:** For role/team/department separation
        - **Dashed boundary boxes:** For system boundaries (C4, deployment)
        - **Solid containers:** For subsystems and modules
        - **Background rectangles:** For visual grouping without containment semantics

        Container style: `container=1;collapsible=0;` added to the group shape.

        ### 6.4 Grid Alignment

        - Always snap to grid: `grid="1" gridSize="10"` in mxGraphModel.
        - Position all shapes at coordinates divisible by 10.
        - Keep widths and heights as multiples of 10.
        - Center-align shapes vertically when in the same row.
        - Left-align shape text content by default.

        ### 6.5 Legend/Key

        Include a legend when using more than 3 colors or symbols:

        ```xml
        <!-- Legend container -->
        <mxCell id="legend" value="Legend" style="swimlane;startSize=20;rounded=1;arcSize=10;fillColor=#f5f5f5;strokeColor=#666666;fontStyle=1;fontSize=12;" parent="1" vertex="1">
          <mxGeometry x="800" y="40" width="200" height="150" as="geometry" />
        </mxCell>
        <!-- Legend items inside -->
        <mxCell id="leg1" value="Process Step" style="rounded=1;whiteSpace=wrap;html=1;fillColor=#dae8fc;strokeColor=#6c8ebf;fontSize=10;" parent="legend" vertex="1">
          <mxGeometry x="10" y="30" width="80" height="25" as="geometry" />
        </mxCell>
        ```

        Place legends in the top-right or bottom-right corner.

        ## 7. Complete XML Templates

        ### 7.1 Flowchart Template

        ```xml
        <mxfile host="app.diagrams.net" type="device">
          <diagram name="Flowchart" id="flowchart1">
            <mxGraphModel dx="1422" dy="794" grid="1" gridSize="10" guides="1" tooltips="1" connect="1" arrows="1" fold="1" page="1" pageScale="1" pageWidth="1169" pageHeight="827" math="0" shadow="0">
              <root>
                <mxCell id="0" />
                <mxCell id="1" parent="0" />
                <mxCell id="2" value="" style="ellipse;fillColor=#000000;strokeColor=#000000;" parent="1" vertex="1">
                  <mxGeometry x="525" y="40" width="30" height="30" as="geometry" />
                </mxCell>
                <mxCell id="3" value="Process Step 1" style="rounded=1;whiteSpace=wrap;html=1;absoluteArcSize=1;arcSize=10;fillColor=#dae8fc;strokeColor=#6c8ebf;" parent="1" vertex="1">
                  <mxGeometry x="480" y="110" width="120" height="40" as="geometry" />
                </mxCell>
                <mxCell id="4" value="Decision?" style="rhombus;whiteSpace=wrap;html=1;fillColor=#fff2cc;strokeColor=#d6b656;" parent="1" vertex="1">
                  <mxGeometry x="500" y="190" width="80" height="60" as="geometry" />
                </mxCell>
                <mxCell id="5" value="Process Step 2A" style="rounded=1;whiteSpace=wrap;html=1;absoluteArcSize=1;arcSize=10;fillColor=#dae8fc;strokeColor=#6c8ebf;" parent="1" vertex="1">
                  <mxGeometry x="340" y="300" width="120" height="40" as="geometry" />
                </mxCell>
                <mxCell id="6" value="Process Step 2B" style="rounded=1;whiteSpace=wrap;html=1;absoluteArcSize=1;arcSize=10;fillColor=#dae8fc;strokeColor=#6c8ebf;" parent="1" vertex="1">
                  <mxGeometry x="620" y="300" width="120" height="40" as="geometry" />
                </mxCell>
                <mxCell id="7" value="" style="ellipse;html=1;shape=endState;fillColor=#000000;strokeColor=#000000;" parent="1" vertex="1">
                  <mxGeometry x="525" y="400" width="30" height="30" as="geometry" />
                </mxCell>
                <mxCell id="8" style="edgeStyle=orthogonalEdgeStyle;rounded=0;orthogonalLoop=1;jettySize=auto;html=1;" parent="1" source="2" target="3" edge="1">
                  <mxGeometry relative="1" as="geometry" />
                </mxCell>
                <mxCell id="9" style="edgeStyle=orthogonalEdgeStyle;rounded=0;orthogonalLoop=1;jettySize=auto;html=1;" parent="1" source="3" target="4" edge="1">
                  <mxGeometry relative="1" as="geometry" />
                </mxCell>
                <mxCell id="10" value="Yes" style="edgeStyle=orthogonalEdgeStyle;rounded=0;orthogonalLoop=1;jettySize=auto;html=1;" parent="1" source="4" target="5" edge="1">
                  <mxGeometry relative="1" as="geometry" />
                </mxCell>
                <mxCell id="11" value="No" style="edgeStyle=orthogonalEdgeStyle;rounded=0;orthogonalLoop=1;jettySize=auto;html=1;" parent="1" source="4" target="6" edge="1">
                  <mxGeometry relative="1" as="geometry" />
                </mxCell>
                <mxCell id="12" style="edgeStyle=orthogonalEdgeStyle;rounded=0;orthogonalLoop=1;jettySize=auto;html=1;" parent="1" source="5" target="7" edge="1">
                  <mxGeometry relative="1" as="geometry" />
                </mxCell>
                <mxCell id="13" style="edgeStyle=orthogonalEdgeStyle;rounded=0;orthogonalLoop=1;jettySize=auto;html=1;" parent="1" source="6" target="7" edge="1">
                  <mxGeometry relative="1" as="geometry" />
                </mxCell>
              </root>
            </mxGraphModel>
          </diagram>
        </mxfile>
        ```

        ### 7.2 UML Class Diagram Template

        ```xml
        <mxfile host="app.diagrams.net" type="device">
          <diagram name="Class Diagram" id="classdiag1">
            <mxGraphModel dx="1422" dy="794" grid="1" gridSize="10" guides="1" tooltips="1" connect="1" arrows="1" fold="1" page="1" pageScale="1" pageWidth="1169" pageHeight="827" math="0" shadow="0">
              <root>
                <mxCell id="0" />
                <mxCell id="1" parent="0" />
                <mxCell id="2" value="Person" style="swimlane;fontStyle=1;align=center;verticalAlign=top;childLayout=stackLayout;horizontal=1;startSize=26;horizontalStack=0;resizeParent=1;resizeLast=0;collapsible=1;marginBottom=0;rounded=0;shadow=0;strokeWidth=1;fillColor=#dae8fc;strokeColor=#6c8ebf;" parent="1" vertex="1">
                  <mxGeometry x="100" y="100" width="160" height="138" as="geometry" />
                </mxCell>
                <mxCell id="3" value="+name: string" style="text;align=left;verticalAlign=top;spacingLeft=4;spacingRight=4;overflow=hidden;rotatable=0;points=[[0,0.5],[1,0.5]];portConstraint=eastwest;" parent="2" vertex="1">
                  <mxGeometry y="26" width="160" height="26" as="geometry" />
                </mxCell>
                <mxCell id="4" value="+email: string" style="text;align=left;verticalAlign=top;spacingLeft=4;spacingRight=4;overflow=hidden;rotatable=0;points=[[0,0.5],[1,0.5]];portConstraint=eastwest;" parent="2" vertex="1">
                  <mxGeometry y="52" width="160" height="26" as="geometry" />
                </mxCell>
                <mxCell id="5" value="" style="line;html=1;strokeWidth=1;align=left;verticalAlign=middle;spacingTop=-1;spacingLeft=3;spacingRight=3;rotatable=0;labelPosition=right;points=[];portConstraint=eastwest;" parent="2" vertex="1">
                  <mxGeometry y="78" width="160" height="8" as="geometry" />
                </mxCell>
                <mxCell id="6" value="+getName(): string" style="text;align=left;verticalAlign=top;spacingLeft=4;spacingRight=4;overflow=hidden;rotatable=0;points=[[0,0.5],[1,0.5]];portConstraint=eastwest;" parent="2" vertex="1">
                  <mxGeometry y="86" width="160" height="26" as="geometry" />
                </mxCell>
                <mxCell id="7" value="+setEmail(email): void" style="text;align=left;verticalAlign=top;spacingLeft=4;spacingRight=4;overflow=hidden;rotatable=0;points=[[0,0.5],[1,0.5]];portConstraint=eastwest;" parent="2" vertex="1">
                  <mxGeometry y="112" width="160" height="26" as="geometry" />
                </mxCell>
                <mxCell id="8" value="Student" style="swimlane;fontStyle=1;align=center;verticalAlign=top;childLayout=stackLayout;horizontal=1;startSize=26;horizontalStack=0;resizeParent=1;resizeLast=0;collapsible=1;marginBottom=0;rounded=0;shadow=0;strokeWidth=1;fillColor=#d5e8d4;strokeColor=#82b366;" parent="1" vertex="1">
                  <mxGeometry x="100" y="320" width="160" height="112" as="geometry" />
                </mxCell>
                <mxCell id="9" value="+studentId: int" style="text;align=left;verticalAlign=top;spacingLeft=4;spacingRight=4;overflow=hidden;rotatable=0;points=[[0,0.5],[1,0.5]];portConstraint=eastwest;" parent="8" vertex="1">
                  <mxGeometry y="26" width="160" height="26" as="geometry" />
                </mxCell>
                <mxCell id="10" value="" style="line;html=1;strokeWidth=1;align=left;verticalAlign=middle;spacingTop=-1;spacingLeft=3;spacingRight=3;rotatable=0;labelPosition=right;points=[];portConstraint=eastwest;" parent="8" vertex="1">
                  <mxGeometry y="52" width="160" height="8" as="geometry" />
                </mxCell>
                <mxCell id="11" value="+enroll(course): void" style="text;align=left;verticalAlign=top;spacingLeft=4;spacingRight=4;overflow=hidden;rotatable=0;points=[[0,0.5],[1,0.5]];portConstraint=eastwest;" parent="8" vertex="1">
                  <mxGeometry y="60" width="160" height="26" as="geometry" />
                </mxCell>
                <mxCell id="12" value="+getGPA(): double" style="text;align=left;verticalAlign=top;spacingLeft=4;spacingRight=4;overflow=hidden;rotatable=0;points=[[0,0.5],[1,0.5]];portConstraint=eastwest;" parent="8" vertex="1">
                  <mxGeometry y="86" width="160" height="26" as="geometry" />
                </mxCell>
                <mxCell id="13" style="endArrow=block;endSize=10;endFill=0;shadow=0;strokeWidth=1;rounded=0;edgeStyle=elbowEdgeStyle;elbow=vertical;" parent="1" source="8" target="2" edge="1">
                  <mxGeometry width="160" relative="1" as="geometry" />
                </mxCell>
              </root>
            </mxGraphModel>
          </diagram>
        </mxfile>
        ```

        ### 7.3 Sequence Diagram Template

        ```xml
        <mxfile host="app.diagrams.net" type="device">
          <diagram name="Sequence Diagram" id="seqdiag1">
            <mxGraphModel dx="1422" dy="794" grid="1" gridSize="10" guides="1" tooltips="1" connect="1" arrows="1" fold="1" page="1" pageScale="1" pageWidth="1169" pageHeight="827" math="0" shadow="0">
              <root>
                <mxCell id="0" />
                <mxCell id="1" parent="0" />
                <mxCell id="2" value="Client" style="shape=umlLifeline;perimeter=lifelinePerimeter;whiteSpace=wrap;html=1;container=1;dropTarget=0;collapsible=0;recursiveResize=0;outlineConnect=0;portConstraint=eastwest;fillColor=#dae8fc;strokeColor=#6c8ebf;" parent="1" vertex="1">
                  <mxGeometry x="100" y="40" width="100" height="400" as="geometry" />
                </mxCell>
                <mxCell id="3" value="" style="html=1;points=[];perimeter=orthogonalPerimeter;outlineConnect=0;targetShapes=umlLifeline;portConstraint=eastwest;fillColor=#dae8fc;strokeColor=#6c8ebf;" parent="2" vertex="1">
                  <mxGeometry x="45" y="80" width="10" height="280" as="geometry" />
                </mxCell>
                <mxCell id="4" value="Server" style="shape=umlLifeline;perimeter=lifelinePerimeter;whiteSpace=wrap;html=1;container=1;dropTarget=0;collapsible=0;recursiveResize=0;outlineConnect=0;portConstraint=eastwest;fillColor=#d5e8d4;strokeColor=#82b366;" parent="1" vertex="1">
                  <mxGeometry x="340" y="40" width="100" height="400" as="geometry" />
                </mxCell>
                <mxCell id="5" value="" style="html=1;points=[];perimeter=orthogonalPerimeter;outlineConnect=0;targetShapes=umlLifeline;portConstraint=eastwest;fillColor=#d5e8d4;strokeColor=#82b366;" parent="4" vertex="1">
                  <mxGeometry x="45" y="110" width="10" height="200" as="geometry" />
                </mxCell>
                <mxCell id="6" value="Database" style="shape=umlLifeline;perimeter=lifelinePerimeter;whiteSpace=wrap;html=1;container=1;dropTarget=0;collapsible=0;recursiveResize=0;outlineConnect=0;portConstraint=eastwest;fillColor=#b1ddf0;strokeColor=#10739e;" parent="1" vertex="1">
                  <mxGeometry x="580" y="40" width="100" height="400" as="geometry" />
                </mxCell>
                <mxCell id="7" value="" style="html=1;points=[];perimeter=orthogonalPerimeter;outlineConnect=0;targetShapes=umlLifeline;portConstraint=eastwest;fillColor=#b1ddf0;strokeColor=#10739e;" parent="6" vertex="1">
                  <mxGeometry x="45" y="160" width="10" height="100" as="geometry" />
                </mxCell>
                <mxCell id="8" value="request()" style="html=1;verticalAlign=bottom;endArrow=block;curved=0;rounded=0;endFill=1;" parent="1" source="3" target="5" edge="1">
                  <mxGeometry relative="1" as="geometry">
                    <mxPoint x="155" y="150" as="sourcePoint" />
                    <mxPoint x="385" y="150" as="targetPoint" />
                  </mxGeometry>
                </mxCell>
                <mxCell id="9" value="query()" style="html=1;verticalAlign=bottom;endArrow=block;curved=0;rounded=0;endFill=1;" parent="1" source="5" target="7" edge="1">
                  <mxGeometry relative="1" as="geometry">
                    <mxPoint x="395" y="200" as="sourcePoint" />
                    <mxPoint x="625" y="200" as="targetPoint" />
                  </mxGeometry>
                </mxCell>
                <mxCell id="10" value="resultSet" style="html=1;verticalAlign=bottom;endArrow=open;curved=0;rounded=0;dashed=1;endFill=0;" parent="1" source="7" target="5" edge="1">
                  <mxGeometry relative="1" as="geometry">
                    <mxPoint x="625" y="240" as="sourcePoint" />
                    <mxPoint x="395" y="240" as="targetPoint" />
                  </mxGeometry>
                </mxCell>
                <mxCell id="11" value="response" style="html=1;verticalAlign=bottom;endArrow=open;curved=0;rounded=0;dashed=1;endFill=0;" parent="1" source="5" target="3" edge="1">
                  <mxGeometry relative="1" as="geometry">
                    <mxPoint x="385" y="300" as="sourcePoint" />
                    <mxPoint x="155" y="300" as="targetPoint" />
                  </mxGeometry>
                </mxCell>
              </root>
            </mxGraphModel>
          </diagram>
        </mxfile>
        ```

        ### 7.4 Entity-Relationship Diagram Template

        ```xml
        <mxfile host="app.diagrams.net" type="device">
          <diagram name="ER Diagram" id="erdiag1">
            <mxGraphModel dx="1422" dy="794" grid="1" gridSize="10" guides="1" tooltips="1" connect="1" arrows="1" fold="1" page="1" pageScale="1" pageWidth="1169" pageHeight="827" math="0" shadow="0">
              <root>
                <mxCell id="0" />
                <mxCell id="1" parent="0" />
                <mxCell id="2" value="Customer" style="swimlane;fontStyle=1;align=center;verticalAlign=top;childLayout=stackLayout;horizontal=1;startSize=26;horizontalStack=0;resizeParent=1;resizeLast=0;collapsible=0;marginBottom=0;rounded=0;shadow=0;strokeWidth=1;fillColor=#dae8fc;strokeColor=#6c8ebf;" parent="1" vertex="1">
                  <mxGeometry x="100" y="100" width="180" height="138" as="geometry" />
                </mxCell>
                <mxCell id="3" value="PK  customer_id: int" style="text;align=left;verticalAlign=top;spacingLeft=4;spacingRight=4;overflow=hidden;rotatable=0;points=[[0,0.5],[1,0.5]];portConstraint=eastwest;fontStyle=4;" parent="2" vertex="1">
                  <mxGeometry y="26" width="180" height="26" as="geometry" />
                </mxCell>
                <mxCell id="4" value="" style="line;html=1;strokeWidth=1;align=left;verticalAlign=middle;spacingTop=-1;spacingLeft=3;spacingRight=3;rotatable=0;labelPosition=right;points=[];portConstraint=eastwest;" parent="2" vertex="1">
                  <mxGeometry y="52" width="180" height="8" as="geometry" />
                </mxCell>
                <mxCell id="5" value="     name: varchar(100)" style="text;align=left;verticalAlign=top;spacingLeft=4;spacingRight=4;overflow=hidden;rotatable=0;points=[[0,0.5],[1,0.5]];portConstraint=eastwest;" parent="2" vertex="1">
                  <mxGeometry y="60" width="180" height="26" as="geometry" />
                </mxCell>
                <mxCell id="6" value="     email: varchar(255)" style="text;align=left;verticalAlign=top;spacingLeft=4;spacingRight=4;overflow=hidden;rotatable=0;points=[[0,0.5],[1,0.5]];portConstraint=eastwest;" parent="2" vertex="1">
                  <mxGeometry y="86" width="180" height="26" as="geometry" />
                </mxCell>
                <mxCell id="7" value="     phone: varchar(20)" style="text;align=left;verticalAlign=top;spacingLeft=4;spacingRight=4;overflow=hidden;rotatable=0;points=[[0,0.5],[1,0.5]];portConstraint=eastwest;" parent="2" vertex="1">
                  <mxGeometry y="112" width="180" height="26" as="geometry" />
                </mxCell>
                <mxCell id="8" value="Order" style="swimlane;fontStyle=1;align=center;verticalAlign=top;childLayout=stackLayout;horizontal=1;startSize=26;horizontalStack=0;resizeParent=1;resizeLast=0;collapsible=0;marginBottom=0;rounded=0;shadow=0;strokeWidth=1;fillColor=#d5e8d4;strokeColor=#82b366;" parent="1" vertex="1">
                  <mxGeometry x="500" y="100" width="180" height="164" as="geometry" />
                </mxCell>
                <mxCell id="9" value="PK  order_id: int" style="text;align=left;verticalAlign=top;spacingLeft=4;spacingRight=4;overflow=hidden;rotatable=0;points=[[0,0.5],[1,0.5]];portConstraint=eastwest;fontStyle=4;" parent="8" vertex="1">
                  <mxGeometry y="26" width="180" height="26" as="geometry" />
                </mxCell>
                <mxCell id="10" value="FK  customer_id: int" style="text;align=left;verticalAlign=top;spacingLeft=4;spacingRight=4;overflow=hidden;rotatable=0;points=[[0,0.5],[1,0.5]];portConstraint=eastwest;" parent="8" vertex="1">
                  <mxGeometry y="52" width="180" height="26" as="geometry" />
                </mxCell>
                <mxCell id="11" value="" style="line;html=1;strokeWidth=1;align=left;verticalAlign=middle;spacingTop=-1;spacingLeft=3;spacingRight=3;rotatable=0;labelPosition=right;points=[];portConstraint=eastwest;" parent="8" vertex="1">
                  <mxGeometry y="78" width="180" height="8" as="geometry" />
                </mxCell>
                <mxCell id="12" value="     order_date: datetime" style="text;align=left;verticalAlign=top;spacingLeft=4;spacingRight=4;overflow=hidden;rotatable=0;points=[[0,0.5],[1,0.5]];portConstraint=eastwest;" parent="8" vertex="1">
                  <mxGeometry y="86" width="180" height="26" as="geometry" />
                </mxCell>
                <mxCell id="13" value="     total: decimal(10,2)" style="text;align=left;verticalAlign=top;spacingLeft=4;spacingRight=4;overflow=hidden;rotatable=0;points=[[0,0.5],[1,0.5]];portConstraint=eastwest;" parent="8" vertex="1">
                  <mxGeometry y="112" width="180" height="26" as="geometry" />
                </mxCell>
                <mxCell id="14" value="     status: varchar(20)" style="text;align=left;verticalAlign=top;spacingLeft=4;spacingRight=4;overflow=hidden;rotatable=0;points=[[0,0.5],[1,0.5]];portConstraint=eastwest;" parent="8" vertex="1">
                  <mxGeometry y="138" width="180" height="26" as="geometry" />
                </mxCell>
                <mxCell id="15" value="" style="endArrow=ERmany;startArrow=ERone;endFill=0;startFill=0;edgeStyle=orthogonalEdgeStyle;rounded=0;html=1;" parent="1" source="2" target="8" edge="1">
                  <mxGeometry relative="1" as="geometry" />
                </mxCell>
                <mxCell id="16" value="places" style="edgeLabel;html=1;align=center;verticalAlign=middle;resizable=0;points=[];" parent="15" vertex="1" connectable="0">
                  <mxGeometry x="0.5" relative="1" as="geometry">
                    <mxPoint as="offset" />
                  </mxGeometry>
                </mxCell>
              </root>
            </mxGraphModel>
          </diagram>
        </mxfile>
        ```

        ### 7.5 Network Diagram Template

        ```xml
        <mxfile host="app.diagrams.net" type="device">
          <diagram name="Network Diagram" id="netdiag1">
            <mxGraphModel dx="1422" dy="794" grid="1" gridSize="10" guides="1" tooltips="1" connect="1" arrows="1" fold="1" page="1" pageScale="1" pageWidth="1169" pageHeight="827" math="0" shadow="0">
              <root>
                <mxCell id="0" />
                <mxCell id="1" parent="0" />
                <mxCell id="2" value="Internet" style="ellipse;shape=cloud;whiteSpace=wrap;html=1;fillColor=#dae8fc;strokeColor=#6c8ebf;fontSize=14;fontStyle=1;" parent="1" vertex="1">
                  <mxGeometry x="440" y="40" width="160" height="90" as="geometry" />
                </mxCell>
                <mxCell id="3" value="Firewall" style="rounded=1;whiteSpace=wrap;html=1;fillColor=#f8cecc;strokeColor=#b85450;fontStyle=1;" parent="1" vertex="1">
                  <mxGeometry x="480" y="190" width="80" height="40" as="geometry" />
                </mxCell>
                <mxCell id="4" value="Load Balancer" style="rounded=1;whiteSpace=wrap;html=1;fillColor=#ffe6cc;strokeColor=#d79b00;fontStyle=1;" parent="1" vertex="1">
                  <mxGeometry x="470" y="290" width="100" height="40" as="geometry" />
                </mxCell>
                <mxCell id="5" value="Web Server 1" style="rounded=1;whiteSpace=wrap;html=1;fillColor=#d5e8d4;strokeColor=#82b366;" parent="1" vertex="1">
                  <mxGeometry x="280" y="400" width="120" height="40" as="geometry" />
                </mxCell>
                <mxCell id="6" value="Web Server 2" style="rounded=1;whiteSpace=wrap;html=1;fillColor=#d5e8d4;strokeColor=#82b366;" parent="1" vertex="1">
                  <mxGeometry x="640" y="400" width="120" height="40" as="geometry" />
                </mxCell>
                <mxCell id="7" value="Database&#xa;Primary" style="shape=cylinder3;whiteSpace=wrap;html=1;boundedLbl=1;backgroundOutline=1;size=15;fillColor=#b1ddf0;strokeColor=#10739e;" parent="1" vertex="1">
                  <mxGeometry x="370" y="510" width="100" height="80" as="geometry" />
                </mxCell>
                <mxCell id="8" value="Database&#xa;Replica" style="shape=cylinder3;whiteSpace=wrap;html=1;boundedLbl=1;backgroundOutline=1;size=15;fillColor=#b1ddf0;strokeColor=#10739e;" parent="1" vertex="1">
                  <mxGeometry x="570" y="510" width="100" height="80" as="geometry" />
                </mxCell>
                <mxCell id="9" style="edgeStyle=orthogonalEdgeStyle;rounded=0;orthogonalLoop=1;jettySize=auto;html=1;" parent="1" source="2" target="3" edge="1">
                  <mxGeometry relative="1" as="geometry" />
                </mxCell>
                <mxCell id="10" style="edgeStyle=orthogonalEdgeStyle;rounded=0;orthogonalLoop=1;jettySize=auto;html=1;" parent="1" source="3" target="4" edge="1">
                  <mxGeometry relative="1" as="geometry" />
                </mxCell>
                <mxCell id="11" style="edgeStyle=orthogonalEdgeStyle;rounded=0;orthogonalLoop=1;jettySize=auto;html=1;" parent="1" source="4" target="5" edge="1">
                  <mxGeometry relative="1" as="geometry" />
                </mxCell>
                <mxCell id="12" style="edgeStyle=orthogonalEdgeStyle;rounded=0;orthogonalLoop=1;jettySize=auto;html=1;" parent="1" source="4" target="6" edge="1">
                  <mxGeometry relative="1" as="geometry" />
                </mxCell>
                <mxCell id="13" style="edgeStyle=orthogonalEdgeStyle;rounded=0;orthogonalLoop=1;jettySize=auto;html=1;" parent="1" source="5" target="7" edge="1">
                  <mxGeometry relative="1" as="geometry" />
                </mxCell>
                <mxCell id="14" style="edgeStyle=orthogonalEdgeStyle;rounded=0;orthogonalLoop=1;jettySize=auto;html=1;" parent="1" source="6" target="8" edge="1">
                  <mxGeometry relative="1" as="geometry" />
                </mxCell>
                <mxCell id="15" value="replication" style="edgeStyle=orthogonalEdgeStyle;rounded=0;orthogonalLoop=1;jettySize=auto;html=1;dashed=1;endArrow=open;endFill=0;" parent="1" source="7" target="8" edge="1">
                  <mxGeometry relative="1" as="geometry" />
                </mxCell>
              </root>
            </mxGraphModel>
          </diagram>
        </mxfile>
        ```

        ### 7.6 Swimlane Diagram Template

        ```xml
        <mxfile host="app.diagrams.net" type="device">
          <diagram name="Swimlane" id="swimlane1">
            <mxGraphModel dx="1422" dy="794" grid="1" gridSize="10" guides="1" tooltips="1" connect="1" arrows="1" fold="1" page="1" pageScale="1" pageWidth="1169" pageHeight="827" math="0" shadow="0">
              <root>
                <mxCell id="0" />
                <mxCell id="1" parent="0" />
                <mxCell id="2" value="Order Processing" style="swimlane;html=1;childLayout=stackLayout;resizeParent=1;resizeParentMax=0;horizontal=0;startSize=30;horizontalStack=0;fontStyle=1;fontSize=14;" parent="1" vertex="1">
                  <mxGeometry x="40" y="40" width="900" height="500" as="geometry" />
                </mxCell>
                <mxCell id="3" value="Customer" style="swimlane;html=1;startSize=30;horizontal=0;fillColor=#dae8fc;strokeColor=#6c8ebf;" parent="2" vertex="1">
                  <mxGeometry y="30" width="900" height="150" as="geometry" />
                </mxCell>
                <mxCell id="4" value="Place Order" style="rounded=1;whiteSpace=wrap;html=1;absoluteArcSize=1;arcSize=10;fillColor=#dae8fc;strokeColor=#6c8ebf;" parent="3" vertex="1">
                  <mxGeometry x="60" y="50" width="100" height="40" as="geometry" />
                </mxCell>
                <mxCell id="5" value="Receive&#xa;Confirmation" style="rounded=1;whiteSpace=wrap;html=1;absoluteArcSize=1;arcSize=10;fillColor=#dae8fc;strokeColor=#6c8ebf;" parent="3" vertex="1">
                  <mxGeometry x="700" y="50" width="100" height="40" as="geometry" />
                </mxCell>
                <mxCell id="6" value="Sales" style="swimlane;html=1;startSize=30;horizontal=0;fillColor=#d5e8d4;strokeColor=#82b366;" parent="2" vertex="1">
                  <mxGeometry y="180" width="900" height="150" as="geometry" />
                </mxCell>
                <mxCell id="7" value="Validate Order" style="rounded=1;whiteSpace=wrap;html=1;absoluteArcSize=1;arcSize=10;fillColor=#d5e8d4;strokeColor=#82b366;" parent="6" vertex="1">
                  <mxGeometry x="220" y="50" width="100" height="40" as="geometry" />
                </mxCell>
                <mxCell id="8" value="Approved?" style="rhombus;whiteSpace=wrap;html=1;fillColor=#fff2cc;strokeColor=#d6b656;" parent="6" vertex="1">
                  <mxGeometry x="380" y="35" width="70" height="60" as="geometry" />
                </mxCell>
                <mxCell id="9" value="Fulfillment" style="swimlane;html=1;startSize=30;horizontal=0;fillColor=#e1d5e7;strokeColor=#9673a6;" parent="2" vertex="1">
                  <mxGeometry y="330" width="900" height="170" as="geometry" />
                </mxCell>
                <mxCell id="10" value="Pick &amp; Pack" style="rounded=1;whiteSpace=wrap;html=1;absoluteArcSize=1;arcSize=10;fillColor=#e1d5e7;strokeColor=#9673a6;" parent="9" vertex="1">
                  <mxGeometry x="520" y="60" width="100" height="40" as="geometry" />
                </mxCell>
                <mxCell id="11" value="Ship" style="rounded=1;whiteSpace=wrap;html=1;absoluteArcSize=1;arcSize=10;fillColor=#e1d5e7;strokeColor=#9673a6;" parent="9" vertex="1">
                  <mxGeometry x="700" y="60" width="100" height="40" as="geometry" />
                </mxCell>
                <mxCell id="12" style="edgeStyle=orthogonalEdgeStyle;rounded=0;orthogonalLoop=1;jettySize=auto;html=1;" parent="2" source="4" target="7" edge="1">
                  <mxGeometry relative="1" as="geometry" />
                </mxCell>
                <mxCell id="13" style="edgeStyle=orthogonalEdgeStyle;rounded=0;orthogonalLoop=1;jettySize=auto;html=1;" parent="2" source="7" target="8" edge="1">
                  <mxGeometry relative="1" as="geometry" />
                </mxCell>
                <mxCell id="14" value="Yes" style="edgeStyle=orthogonalEdgeStyle;rounded=0;orthogonalLoop=1;jettySize=auto;html=1;" parent="2" source="8" target="10" edge="1">
                  <mxGeometry relative="1" as="geometry" />
                </mxCell>
                <mxCell id="15" style="edgeStyle=orthogonalEdgeStyle;rounded=0;orthogonalLoop=1;jettySize=auto;html=1;" parent="2" source="10" target="11" edge="1">
                  <mxGeometry relative="1" as="geometry" />
                </mxCell>
                <mxCell id="16" style="edgeStyle=orthogonalEdgeStyle;rounded=0;orthogonalLoop=1;jettySize=auto;html=1;" parent="2" source="11" target="5" edge="1">
                  <mxGeometry relative="1" as="geometry" />
                </mxCell>
              </root>
            </mxGraphModel>
          </diagram>
        </mxfile>
        ```

        ### 7.7 C4 Context Diagram Template

        ```xml
        <mxfile host="app.diagrams.net" type="device">
          <diagram name="C4 Context" id="c4ctx1">
            <mxGraphModel dx="1422" dy="794" grid="1" gridSize="10" guides="1" tooltips="1" connect="1" arrows="1" fold="1" page="1" pageScale="1" pageWidth="1169" pageHeight="827" math="0" shadow="0">
              <root>
                <mxCell id="0" />
                <mxCell id="1" parent="0" />
                <mxCell id="2" value="&lt;b&gt;Customer&lt;/b&gt;&lt;br&gt;[Person]&lt;br&gt;&lt;br&gt;Uses the web application to manage orders" style="html=1;fontSize=11;dashed=0;whiteSpace=wrap;fillColor=#083F75;strokeColor=#06315C;fontColor=#ffffff;shape=mxgraph.c4.person2;align=center;metaEdit=1;points=[[0.5,0,0],[1,0.5,0],[1,0.75,0],[0.75,1,0],[0.5,1,0],[0.25,1,0],[0,0.75,0],[0,0.5,0]];resizable=0;" parent="1" vertex="1">
                  <mxGeometry x="460" y="40" width="200" height="180" as="geometry" />
                </mxCell>
                <mxCell id="3" value="&lt;b&gt;Web Application&lt;/b&gt;&lt;br&gt;[Software System]&lt;br&gt;&lt;br&gt;Provides order management functionality" style="rounded=1;whiteSpace=wrap;html=1;labelBackgroundColor=none;fillColor=#1061B0;fontColor=#ffffff;align=center;arcSize=10;strokeColor=#0D5091;metaEdit=1;resizable=0;" parent="1" vertex="1">
                  <mxGeometry x="410" y="310" width="300" height="160" as="geometry" />
                </mxCell>
                <mxCell id="4" value="&lt;b&gt;Email System&lt;/b&gt;&lt;br&gt;[External System]&lt;br&gt;&lt;br&gt;Sends notifications to customers" style="rounded=1;whiteSpace=wrap;html=1;labelBackgroundColor=none;fillColor=#8C8496;fontColor=#ffffff;align=center;arcSize=10;strokeColor=#736782;metaEdit=1;resizable=0;" parent="1" vertex="1">
                  <mxGeometry x="800" y="310" width="300" height="160" as="geometry" />
                </mxCell>
                <mxCell id="5" value="&lt;b&gt;Database&lt;/b&gt;&lt;br&gt;[External System]&lt;br&gt;&lt;br&gt;Stores order and customer data" style="rounded=1;whiteSpace=wrap;html=1;labelBackgroundColor=none;fillColor=#8C8496;fontColor=#ffffff;align=center;arcSize=10;strokeColor=#736782;metaEdit=1;resizable=0;" parent="1" vertex="1">
                  <mxGeometry x="410" y="570" width="300" height="160" as="geometry" />
                </mxCell>
                <mxCell id="6" value="Uses" style="endArrow=block;endSize=10;endFill=1;html=1;rounded=0;strokeWidth=2;strokeColor=#333333;exitX=0.5;exitY=1;exitDx=0;exitDy=0;exitPerimeter=0;" parent="1" source="2" target="3" edge="1">
                  <mxGeometry relative="1" as="geometry" />
                </mxCell>
                <mxCell id="7" value="Sends emails using" style="endArrow=block;endSize=10;endFill=1;html=1;rounded=0;strokeWidth=2;strokeColor=#333333;" parent="1" source="3" target="4" edge="1">
                  <mxGeometry relative="1" as="geometry" />
                </mxCell>
                <mxCell id="8" value="Reads/writes" style="endArrow=block;endSize=10;endFill=1;html=1;rounded=0;strokeWidth=2;strokeColor=#333333;" parent="1" source="3" target="5" edge="1">
                  <mxGeometry relative="1" as="geometry" />
                </mxCell>
              </root>
            </mxGraphModel>
          </diagram>
        </mxfile>
        ```

        ### 7.8 Architecture Diagram Template

        ```xml
        <mxfile host="app.diagrams.net" type="device">
          <diagram name="Architecture" id="archdiag1">
            <mxGraphModel dx="1422" dy="794" grid="1" gridSize="10" guides="1" tooltips="1" connect="1" arrows="1" fold="1" page="1" pageScale="1" pageWidth="1169" pageHeight="827" math="0" shadow="0">
              <root>
                <mxCell id="0" />
                <mxCell id="1" parent="0" />
                <!-- Presentation Layer -->
                <mxCell id="2" value="Presentation Layer" style="rounded=1;whiteSpace=wrap;html=1;arcSize=5;dashed=1;dashPattern=5 2;fillColor=none;strokeColor=#6c8ebf;verticalAlign=top;fontStyle=1;fontSize=14;spacingTop=5;" parent="1" vertex="1">
                  <mxGeometry x="40" y="40" width="400" height="120" as="geometry" />
                </mxCell>
                <mxCell id="3" value="Web App&#xa;(React)" style="rounded=1;whiteSpace=wrap;html=1;fillColor=#dae8fc;strokeColor=#6c8ebf;" parent="1" vertex="1">
                  <mxGeometry x="70" y="80" width="100" height="50" as="geometry" />
                </mxCell>
                <mxCell id="4" value="Mobile App&#xa;(React Native)" style="rounded=1;whiteSpace=wrap;html=1;fillColor=#dae8fc;strokeColor=#6c8ebf;" parent="1" vertex="1">
                  <mxGeometry x="210" y="80" width="100" height="50" as="geometry" />
                </mxCell>
                <!-- API Layer -->
                <mxCell id="5" value="API Layer" style="rounded=1;whiteSpace=wrap;html=1;arcSize=5;dashed=1;dashPattern=5 2;fillColor=none;strokeColor=#82b366;verticalAlign=top;fontStyle=1;fontSize=14;spacingTop=5;" parent="1" vertex="1">
                  <mxGeometry x="40" y="220" width="400" height="120" as="geometry" />
                </mxCell>
                <mxCell id="6" value="API Gateway" style="rounded=1;whiteSpace=wrap;html=1;fillColor=#d5e8d4;strokeColor=#82b366;" parent="1" vertex="1">
                  <mxGeometry x="170" y="260" width="100" height="50" as="geometry" />
                </mxCell>
                <!-- Service Layer -->
                <mxCell id="7" value="Service Layer" style="rounded=1;whiteSpace=wrap;html=1;arcSize=5;dashed=1;dashPattern=5 2;fillColor=none;strokeColor=#d79b00;verticalAlign=top;fontStyle=1;fontSize=14;spacingTop=5;" parent="1" vertex="1">
                  <mxGeometry x="40" y="400" width="400" height="120" as="geometry" />
                </mxCell>
                <mxCell id="8" value="Auth Service" style="rounded=1;whiteSpace=wrap;html=1;fillColor=#ffe6cc;strokeColor=#d79b00;" parent="1" vertex="1">
                  <mxGeometry x="60" y="440" width="100" height="50" as="geometry" />
                </mxCell>
                <mxCell id="9" value="Order Service" style="rounded=1;whiteSpace=wrap;html=1;fillColor=#ffe6cc;strokeColor=#d79b00;" parent="1" vertex="1">
                  <mxGeometry x="190" y="440" width="100" height="50" as="geometry" />
                </mxCell>
                <mxCell id="10" value="Notification&#xa;Service" style="rounded=1;whiteSpace=wrap;html=1;fillColor=#ffe6cc;strokeColor=#d79b00;" parent="1" vertex="1">
                  <mxGeometry x="320" y="440" width="100" height="50" as="geometry" />
                </mxCell>
                <!-- Data Layer -->
                <mxCell id="11" value="Data Layer" style="rounded=1;whiteSpace=wrap;html=1;arcSize=5;dashed=1;dashPattern=5 2;fillColor=none;strokeColor=#10739e;verticalAlign=top;fontStyle=1;fontSize=14;spacingTop=5;" parent="1" vertex="1">
                  <mxGeometry x="40" y="580" width="400" height="140" as="geometry" />
                </mxCell>
                <mxCell id="12" value="PostgreSQL" style="shape=cylinder3;whiteSpace=wrap;html=1;boundedLbl=1;backgroundOutline=1;size=15;fillColor=#b1ddf0;strokeColor=#10739e;" parent="1" vertex="1">
                  <mxGeometry x="80" y="620" width="80" height="70" as="geometry" />
                </mxCell>
                <mxCell id="13" value="Redis Cache" style="shape=cylinder3;whiteSpace=wrap;html=1;boundedLbl=1;backgroundOutline=1;size=15;fillColor=#b1ddf0;strokeColor=#10739e;" parent="1" vertex="1">
                  <mxGeometry x="200" y="620" width="80" height="70" as="geometry" />
                </mxCell>
                <mxCell id="14" value="S3 Storage" style="shape=cylinder3;whiteSpace=wrap;html=1;boundedLbl=1;backgroundOutline=1;size=15;fillColor=#b1ddf0;strokeColor=#10739e;" parent="1" vertex="1">
                  <mxGeometry x="320" y="620" width="80" height="70" as="geometry" />
                </mxCell>
                <!-- Connectors -->
                <mxCell id="15" style="edgeStyle=orthogonalEdgeStyle;rounded=0;orthogonalLoop=1;jettySize=auto;html=1;" parent="1" source="3" target="6" edge="1">
                  <mxGeometry relative="1" as="geometry" />
                </mxCell>
                <mxCell id="16" style="edgeStyle=orthogonalEdgeStyle;rounded=0;orthogonalLoop=1;jettySize=auto;html=1;" parent="1" source="4" target="6" edge="1">
                  <mxGeometry relative="1" as="geometry" />
                </mxCell>
                <mxCell id="17" style="edgeStyle=orthogonalEdgeStyle;rounded=0;orthogonalLoop=1;jettySize=auto;html=1;" parent="1" source="6" target="8" edge="1">
                  <mxGeometry relative="1" as="geometry" />
                </mxCell>
                <mxCell id="18" style="edgeStyle=orthogonalEdgeStyle;rounded=0;orthogonalLoop=1;jettySize=auto;html=1;" parent="1" source="6" target="9" edge="1">
                  <mxGeometry relative="1" as="geometry" />
                </mxCell>
                <mxCell id="19" style="edgeStyle=orthogonalEdgeStyle;rounded=0;orthogonalLoop=1;jettySize=auto;html=1;" parent="1" source="6" target="10" edge="1">
                  <mxGeometry relative="1" as="geometry" />
                </mxCell>
                <mxCell id="20" style="edgeStyle=orthogonalEdgeStyle;rounded=0;orthogonalLoop=1;jettySize=auto;html=1;" parent="1" source="9" target="12" edge="1">
                  <mxGeometry relative="1" as="geometry" />
                </mxCell>
                <mxCell id="21" style="edgeStyle=orthogonalEdgeStyle;rounded=0;orthogonalLoop=1;jettySize=auto;html=1;dashed=1;" parent="1" source="8" target="13" edge="1">
                  <mxGeometry relative="1" as="geometry" />
                </mxCell>
              </root>
            </mxGraphModel>
          </diagram>
        </mxfile>
        ```

        ### 7.9 Org Chart Template

        ```xml
        <mxfile host="app.diagrams.net" type="device">
          <diagram name="Org Chart" id="orgchart1">
            <mxGraphModel dx="1422" dy="794" grid="1" gridSize="10" guides="1" tooltips="1" connect="1" arrows="1" fold="1" page="1" pageScale="1" pageWidth="1169" pageHeight="827" math="0" shadow="0">
              <root>
                <mxCell id="0" />
                <mxCell id="1" parent="0" />
                <mxCell id="2" value="&lt;b&gt;Jane Smith&lt;/b&gt;&lt;br&gt;CEO" style="rounded=1;whiteSpace=wrap;html=1;arcSize=10;fillColor=#6c8ebf;strokeColor=#6c8ebf;fontColor=#ffffff;shadow=1;fontSize=12;" parent="1" vertex="1">
                  <mxGeometry x="440" y="40" width="180" height="60" as="geometry" />
                </mxCell>
                <mxCell id="3" value="&lt;b&gt;John Doe&lt;/b&gt;&lt;br&gt;CTO" style="rounded=1;whiteSpace=wrap;html=1;arcSize=10;fillColor=#dae8fc;strokeColor=#6c8ebf;shadow=1;fontSize=11;" parent="1" vertex="1">
                  <mxGeometry x="200" y="160" width="160" height="50" as="geometry" />
                </mxCell>
                <mxCell id="4" value="&lt;b&gt;Alice Brown&lt;/b&gt;&lt;br&gt;CFO" style="rounded=1;whiteSpace=wrap;html=1;arcSize=10;fillColor=#dae8fc;strokeColor=#6c8ebf;shadow=1;fontSize=11;" parent="1" vertex="1">
                  <mxGeometry x="450" y="160" width="160" height="50" as="geometry" />
                </mxCell>
                <mxCell id="5" value="&lt;b&gt;Bob Wilson&lt;/b&gt;&lt;br&gt;COO" style="rounded=1;whiteSpace=wrap;html=1;arcSize=10;fillColor=#dae8fc;strokeColor=#6c8ebf;shadow=1;fontSize=11;" parent="1" vertex="1">
                  <mxGeometry x="700" y="160" width="160" height="50" as="geometry" />
                </mxCell>
                <mxCell id="6" value="&lt;b&gt;Dev Team&lt;/b&gt;&lt;br&gt;5 Engineers" style="rounded=1;whiteSpace=wrap;html=1;arcSize=10;fillColor=#d5e8d4;strokeColor=#82b366;shadow=1;fontSize=10;" parent="1" vertex="1">
                  <mxGeometry x="120" y="280" width="140" height="45" as="geometry" />
                </mxCell>
                <mxCell id="7" value="&lt;b&gt;QA Team&lt;/b&gt;&lt;br&gt;3 Testers" style="rounded=1;whiteSpace=wrap;html=1;arcSize=10;fillColor=#d5e8d4;strokeColor=#82b366;shadow=1;fontSize=10;" parent="1" vertex="1">
                  <mxGeometry x="300" y="280" width="140" height="45" as="geometry" />
                </mxCell>
                <mxCell id="8" style="edgeStyle=orthogonalEdgeStyle;rounded=1;orthogonalLoop=1;jettySize=auto;html=1;endArrow=none;endFill=0;strokeWidth=1;strokeColor=#999999;" parent="1" source="2" target="3" edge="1">
                  <mxGeometry relative="1" as="geometry" />
                </mxCell>
                <mxCell id="9" style="edgeStyle=orthogonalEdgeStyle;rounded=1;orthogonalLoop=1;jettySize=auto;html=1;endArrow=none;endFill=0;strokeWidth=1;strokeColor=#999999;" parent="1" source="2" target="4" edge="1">
                  <mxGeometry relative="1" as="geometry" />
                </mxCell>
                <mxCell id="10" style="edgeStyle=orthogonalEdgeStyle;rounded=1;orthogonalLoop=1;jettySize=auto;html=1;endArrow=none;endFill=0;strokeWidth=1;strokeColor=#999999;" parent="1" source="2" target="5" edge="1">
                  <mxGeometry relative="1" as="geometry" />
                </mxCell>
                <mxCell id="11" style="edgeStyle=orthogonalEdgeStyle;rounded=1;orthogonalLoop=1;jettySize=auto;html=1;endArrow=none;endFill=0;strokeWidth=1;strokeColor=#999999;" parent="1" source="3" target="6" edge="1">
                  <mxGeometry relative="1" as="geometry" />
                </mxCell>
                <mxCell id="12" style="edgeStyle=orthogonalEdgeStyle;rounded=1;orthogonalLoop=1;jettySize=auto;html=1;endArrow=none;endFill=0;strokeWidth=1;strokeColor=#999999;" parent="1" source="3" target="7" edge="1">
                  <mxGeometry relative="1" as="geometry" />
                </mxCell>
              </root>
            </mxGraphModel>
          </diagram>
        </mxfile>
        ```

        ### 7.10 Mind Map Template

        ```xml
        <mxfile host="app.diagrams.net" type="device">
          <diagram name="Mind Map" id="mindmap1">
            <mxGraphModel dx="1422" dy="794" grid="1" gridSize="10" guides="1" tooltips="1" connect="1" arrows="1" fold="1" page="1" pageScale="1" pageWidth="1169" pageHeight="827" math="0" shadow="0">
              <root>
                <mxCell id="0" />
                <mxCell id="1" parent="0" />
                <mxCell id="2" value="Project&#xa;Planning" style="ellipse;whiteSpace=wrap;html=1;fillColor=#dae8fc;strokeColor=#6c8ebf;fontSize=16;fontStyle=1;" parent="1" vertex="1">
                  <mxGeometry x="460" y="340" width="160" height="80" as="geometry" />
                </mxCell>
                <mxCell id="3" value="Requirements" style="rounded=1;whiteSpace=wrap;html=1;arcSize=50;fillColor=#d5e8d4;strokeColor=#82b366;fontStyle=1;" parent="1" vertex="1">
                  <mxGeometry x="200" y="150" width="120" height="40" as="geometry" />
                </mxCell>
                <mxCell id="4" value="Design" style="rounded=1;whiteSpace=wrap;html=1;arcSize=50;fillColor=#fff2cc;strokeColor=#d6b656;fontStyle=1;" parent="1" vertex="1">
                  <mxGeometry x="700" y="150" width="120" height="40" as="geometry" />
                </mxCell>
                <mxCell id="5" value="Development" style="rounded=1;whiteSpace=wrap;html=1;arcSize=50;fillColor=#e1d5e7;strokeColor=#9673a6;fontStyle=1;" parent="1" vertex="1">
                  <mxGeometry x="200" y="560" width="120" height="40" as="geometry" />
                </mxCell>
                <mxCell id="6" value="Testing" style="rounded=1;whiteSpace=wrap;html=1;arcSize=50;fillColor=#f8cecc;strokeColor=#b85450;fontStyle=1;" parent="1" vertex="1">
                  <mxGeometry x="700" y="560" width="120" height="40" as="geometry" />
                </mxCell>
                <mxCell id="7" value="User Stories" style="rounded=1;whiteSpace=wrap;html=1;arcSize=50;fillColor=#d5e8d4;strokeColor=#82b366;fontSize=10;" parent="1" vertex="1">
                  <mxGeometry x="60" y="80" width="100" height="30" as="geometry" />
                </mxCell>
                <mxCell id="8" value="Stakeholders" style="rounded=1;whiteSpace=wrap;html=1;arcSize=50;fillColor=#d5e8d4;strokeColor=#82b366;fontSize=10;" parent="1" vertex="1">
                  <mxGeometry x="60" y="130" width="100" height="30" as="geometry" />
                </mxCell>
                <mxCell id="9" value="Constraints" style="rounded=1;whiteSpace=wrap;html=1;arcSize=50;fillColor=#d5e8d4;strokeColor=#82b366;fontSize=10;" parent="1" vertex="1">
                  <mxGeometry x="60" y="180" width="100" height="30" as="geometry" />
                </mxCell>
                <mxCell id="10" value="Architecture" style="rounded=1;whiteSpace=wrap;html=1;arcSize=50;fillColor=#fff2cc;strokeColor=#d6b656;fontSize=10;" parent="1" vertex="1">
                  <mxGeometry x="870" y="100" width="100" height="30" as="geometry" />
                </mxCell>
                <mxCell id="11" value="UI/UX" style="rounded=1;whiteSpace=wrap;html=1;arcSize=50;fillColor=#fff2cc;strokeColor=#d6b656;fontSize=10;" parent="1" vertex="1">
                  <mxGeometry x="870" y="150" width="100" height="30" as="geometry" />
                </mxCell>
                <mxCell id="12" value="Data Model" style="rounded=1;whiteSpace=wrap;html=1;arcSize=50;fillColor=#fff2cc;strokeColor=#d6b656;fontSize=10;" parent="1" vertex="1">
                  <mxGeometry x="870" y="200" width="100" height="30" as="geometry" />
                </mxCell>
                <!-- Branch connectors -->
                <mxCell id="13" style="edgeStyle=entityRelationEdgeStyle;curved=1;rounded=0;html=1;endArrow=none;endFill=0;strokeWidth=2;strokeColor=#82b366;" parent="1" source="2" target="3" edge="1">
                  <mxGeometry relative="1" as="geometry" />
                </mxCell>
                <mxCell id="14" style="edgeStyle=entityRelationEdgeStyle;curved=1;rounded=0;html=1;endArrow=none;endFill=0;strokeWidth=2;strokeColor=#d6b656;" parent="1" source="2" target="4" edge="1">
                  <mxGeometry relative="1" as="geometry" />
                </mxCell>
                <mxCell id="15" style="edgeStyle=entityRelationEdgeStyle;curved=1;rounded=0;html=1;endArrow=none;endFill=0;strokeWidth=2;strokeColor=#9673a6;" parent="1" source="2" target="5" edge="1">
                  <mxGeometry relative="1" as="geometry" />
                </mxCell>
                <mxCell id="16" style="edgeStyle=entityRelationEdgeStyle;curved=1;rounded=0;html=1;endArrow=none;endFill=0;strokeWidth=2;strokeColor=#b85450;" parent="1" source="2" target="6" edge="1">
                  <mxGeometry relative="1" as="geometry" />
                </mxCell>
                <mxCell id="17" style="edgeStyle=entityRelationEdgeStyle;curved=1;rounded=0;html=1;endArrow=none;endFill=0;strokeWidth=1;strokeColor=#82b366;" parent="1" source="3" target="7" edge="1">
                  <mxGeometry relative="1" as="geometry" />
                </mxCell>
                <mxCell id="18" style="edgeStyle=entityRelationEdgeStyle;curved=1;rounded=0;html=1;endArrow=none;endFill=0;strokeWidth=1;strokeColor=#82b366;" parent="1" source="3" target="8" edge="1">
                  <mxGeometry relative="1" as="geometry" />
                </mxCell>
                <mxCell id="19" style="edgeStyle=entityRelationEdgeStyle;curved=1;rounded=0;html=1;endArrow=none;endFill=0;strokeWidth=1;strokeColor=#82b366;" parent="1" source="3" target="9" edge="1">
                  <mxGeometry relative="1" as="geometry" />
                </mxCell>
                <mxCell id="20" style="edgeStyle=entityRelationEdgeStyle;curved=1;rounded=0;html=1;endArrow=none;endFill=0;strokeWidth=1;strokeColor=#d6b656;" parent="1" source="4" target="10" edge="1">
                  <mxGeometry relative="1" as="geometry" />
                </mxCell>
                <mxCell id="21" style="edgeStyle=entityRelationEdgeStyle;curved=1;rounded=0;html=1;endArrow=none;endFill=0;strokeWidth=1;strokeColor=#d6b656;" parent="1" source="4" target="11" edge="1">
                  <mxGeometry relative="1" as="geometry" />
                </mxCell>
                <mxCell id="22" style="edgeStyle=entityRelationEdgeStyle;curved=1;rounded=0;html=1;endArrow=none;endFill=0;strokeWidth=1;strokeColor=#d6b656;" parent="1" source="4" target="12" edge="1">
                  <mxGeometry relative="1" as="geometry" />
                </mxCell>
              </root>
            </mxGraphModel>
          </diagram>
        </mxfile>
        ```

        ## 8. Anti-Patterns and Common Mistakes

        ### 8.1 Missing Root Cells
        **Wrong:** Starting shapes at id="0" or omitting the two root cells.
        **Right:** Always include `<mxCell id="0" />` and `<mxCell id="1" parent="0" />` as the first two cells.

        ### 8.2 Inconsistent Color Usage
        **Wrong:** Using random hex colors or mixing color schemes arbitrarily.
        **Right:** Choose one theme from the official palette. Assign colors by semantic meaning (blue=process, yellow=decision, green=success).

        ### 8.3 Missing Labels on Connectors
        **Wrong:** Leaving all connectors unlabeled, forcing readers to guess the relationship.
        **Right:** Label connectors with action verbs ("calls", "returns", "creates") or conditions ("Yes", "No", "[condition]").

        ### 8.4 Crossing Connector Lines
        **Wrong:** Allowing multiple connectors to cross without rerouting.
        **Right:** Use waypoints, reroute connectors, or use line jumps (`jumpStyle=arc;jumpSize=10;`) to handle necessary crossings.

        ### 8.5 No Visual Hierarchy
        **Wrong:** All shapes same size, same color, same font weight.
        **Right:** Use size, color intensity, and font weight to establish importance. Titles larger and bolder. Primary elements use filled colors. Supporting elements use lighter fills.

        ### 8.6 Misaligned Shapes
        **Wrong:** Shapes placed at arbitrary pixel positions creating a messy appearance.
        **Right:** Enable grid snapping (gridSize=10). Place all shapes at coordinates divisible by 10. Center-align shapes in the same row.

        ### 8.7 Overcrowded Diagrams
        **Wrong:** Cramming 50+ shapes onto a single page with minimal spacing.
        **Right:** Use multi-page diagrams for complex subjects. Limit to 15-20 shapes per page. Use C4 model's progressive detail approach.

        ### 8.8 Incorrect Parent References
        **Wrong:** Using XML nesting for parent-child relationships or forgetting parent attributes on children.
        **Right:** Always use the `parent` attribute to establish containment. Children of containers reference the container's id as parent.

        ### 8.9 Missing Legend
        **Wrong:** Using 4+ colors or symbols without explanation.
        **Right:** Include a legend box in the corner explaining what each color/shape/line style represents.

        ### 8.10 Wrong Flow Direction
        **Wrong:** Mixing flow directions (some arrows going up, some down, some left) within the same diagram.
        **Right:** Maintain consistent flow direction. Top-to-bottom for hierarchies and sequences. Left-to-right for processes and timelines.

        ## 9. Diagram Selection Guide

        | Scenario                              | Recommended Diagram Type        |
        |---------------------------------------|---------------------------------|
        | Show a process or workflow             | Flowchart or Swimlane           |
        | Model software classes/objects         | UML Class Diagram               |
        | Show message passing between objects   | Sequence Diagram                |
        | Design a database schema               | ER Diagram                      |
        | Show system architecture               | Architecture or C4 Diagram      |
        | Visualize network topology             | Network Diagram                 |
        | Brainstorm or explore ideas            | Mind Map                        |
        | Show organizational structure          | Org Chart                       |
        | Model business processes               | BPMN or Swimlane                |
        | Show cross-team responsibilities       | Swimlane Diagram                |
        | Git branching strategy                 | Gitflow Diagram                 |
        | Track project status                   | Kanban Board                    |
        | Show deployment infrastructure         | UML Deployment or AWS Diagram   |
        | State transitions of an object         | UML State Machine               |

        ## 10. Quality Checklist

        Before finalizing any diagram, verify:

        1. [ ] Valid XML with mxfile > diagram > mxGraphModel > root structure
        2. [ ] Root cells id="0" and id="1" present
        3. [ ] Sequential numeric IDs starting from 2
        4. [ ] All shapes have meaningful labels
        5. [ ] Colors from official palette only
        6. [ ] Maximum 3-5 colors used semantically
        7. [ ] Consistent flow direction throughout
        8. [ ] Grid-aligned coordinates (multiples of 10)
        9. [ ] Adequate spacing (40px+ between shapes)
        10. [ ] All connectors have appropriate arrowheads
        11. [ ] Decision connectors labeled with conditions
        12. [ ] Legend included if using 4+ colors/symbols
        13. [ ] No crossing lines (or line jumps used)
        14. [ ] Font sizes follow hierarchy (title > heading > body > annotation)
        15. [ ] Parent references correct for contained shapes
        """;
}

public class SkillDefinition
{
    public required string Name { get; init; }
    public required string Version { get; init; }
    public required string Description { get; init; }
    public required string Instructions { get; init; }
}
