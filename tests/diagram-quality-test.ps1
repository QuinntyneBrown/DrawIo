# Draw.io Diagram Quality Test Harness
# Generates 10 diagram types from skill templates and validates quality across 40 iterations.

$ErrorActionPreference = "Stop"

$tempDir = "C:\projects\DrawIo\tests\temp-diagrams"
if (Test-Path $tempDir) { Remove-Item $tempDir -Recurse -Force }
New-Item -ItemType Directory -Path $tempDir -Force | Out-Null

# Official color palette fill colors
$officialFillColors = @("#dae8fc", "#d5e8d4", "#fff2cc", "#ffe6cc", "#f8cecc", "#e1d5e7", "#b1ddf0")
# Also allow these standard fills used in templates
$extendedFillColors = $officialFillColors + @("#ffffff", "#000000", "#f5f5f5", "#083F75", "#1061B0", "#23A2D9", "#8C8496", "#6c8ebf", "#036897")

# ── Templates ──

$templates = @{}

$templates["flowchart"] = @'
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
'@

$templates["class-diagram"] = @'
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
'@

$templates["sequence-diagram"] = @'
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
'@

$templates["er-diagram"] = @'
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
'@

$templates["network-diagram"] = @'
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
'@

$templates["swimlane"] = @'
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
'@

$templates["c4-context"] = @'
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
'@

$templates["architecture"] = @'
<mxfile host="app.diagrams.net" type="device">
  <diagram name="Architecture" id="archdiag1">
    <mxGraphModel dx="1422" dy="794" grid="1" gridSize="10" guides="1" tooltips="1" connect="1" arrows="1" fold="1" page="1" pageScale="1" pageWidth="1169" pageHeight="827" math="0" shadow="0">
      <root>
        <mxCell id="0" />
        <mxCell id="1" parent="0" />
        <mxCell id="2" value="Presentation Layer" style="rounded=1;whiteSpace=wrap;html=1;arcSize=5;dashed=1;dashPattern=5 2;fillColor=none;strokeColor=#6c8ebf;verticalAlign=top;fontStyle=1;fontSize=14;spacingTop=5;" parent="1" vertex="1">
          <mxGeometry x="40" y="40" width="400" height="120" as="geometry" />
        </mxCell>
        <mxCell id="3" value="Web App&#xa;(React)" style="rounded=1;whiteSpace=wrap;html=1;fillColor=#dae8fc;strokeColor=#6c8ebf;" parent="1" vertex="1">
          <mxGeometry x="70" y="80" width="100" height="50" as="geometry" />
        </mxCell>
        <mxCell id="4" value="Mobile App&#xa;(React Native)" style="rounded=1;whiteSpace=wrap;html=1;fillColor=#dae8fc;strokeColor=#6c8ebf;" parent="1" vertex="1">
          <mxGeometry x="210" y="80" width="100" height="50" as="geometry" />
        </mxCell>
        <mxCell id="5" value="API Layer" style="rounded=1;whiteSpace=wrap;html=1;arcSize=5;dashed=1;dashPattern=5 2;fillColor=none;strokeColor=#82b366;verticalAlign=top;fontStyle=1;fontSize=14;spacingTop=5;" parent="1" vertex="1">
          <mxGeometry x="40" y="220" width="400" height="120" as="geometry" />
        </mxCell>
        <mxCell id="6" value="API Gateway" style="rounded=1;whiteSpace=wrap;html=1;fillColor=#d5e8d4;strokeColor=#82b366;" parent="1" vertex="1">
          <mxGeometry x="170" y="260" width="100" height="50" as="geometry" />
        </mxCell>
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
'@

$templates["org-chart"] = @'
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
'@

$templates["mind-map"] = @'
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
        <mxCell id="20" style="edgeStyle=entityRelationEdgeStyle;curved=1;rounded=0;html=1;endArrow=none;endFill=0;strokeWidth=2;strokeColor=#82b366;" parent="1" source="2" target="3" edge="1">
          <mxGeometry relative="1" as="geometry" />
        </mxCell>
        <mxCell id="21" style="edgeStyle=entityRelationEdgeStyle;curved=1;rounded=0;html=1;endArrow=none;endFill=0;strokeWidth=2;strokeColor=#d6b656;" parent="1" source="2" target="4" edge="1">
          <mxGeometry relative="1" as="geometry" />
        </mxCell>
        <mxCell id="22" style="edgeStyle=entityRelationEdgeStyle;curved=1;rounded=0;html=1;endArrow=none;endFill=0;strokeWidth=2;strokeColor=#9673a6;" parent="1" source="2" target="5" edge="1">
          <mxGeometry relative="1" as="geometry" />
        </mxCell>
        <mxCell id="23" style="edgeStyle=entityRelationEdgeStyle;curved=1;rounded=0;html=1;endArrow=none;endFill=0;strokeWidth=2;strokeColor=#b85450;" parent="1" source="2" target="6" edge="1">
          <mxGeometry relative="1" as="geometry" />
        </mxCell>
        <mxCell id="24" style="edgeStyle=entityRelationEdgeStyle;curved=1;rounded=0;html=1;endArrow=none;endFill=0;strokeWidth=1;strokeColor=#82b366;" parent="1" source="3" target="7" edge="1">
          <mxGeometry relative="1" as="geometry" />
        </mxCell>
        <mxCell id="25" style="edgeStyle=entityRelationEdgeStyle;curved=1;rounded=0;html=1;endArrow=none;endFill=0;strokeWidth=1;strokeColor=#82b366;" parent="1" source="3" target="8" edge="1">
          <mxGeometry relative="1" as="geometry" />
        </mxCell>
        <mxCell id="26" style="edgeStyle=entityRelationEdgeStyle;curved=1;rounded=0;html=1;endArrow=none;endFill=0;strokeWidth=1;strokeColor=#82b366;" parent="1" source="3" target="9" edge="1">
          <mxGeometry relative="1" as="geometry" />
        </mxCell>
        <mxCell id="27" style="edgeStyle=entityRelationEdgeStyle;curved=1;rounded=0;html=1;endArrow=none;endFill=0;strokeWidth=1;strokeColor=#d6b656;" parent="1" source="4" target="10" edge="1">
          <mxGeometry relative="1" as="geometry" />
        </mxCell>
        <mxCell id="28" style="edgeStyle=entityRelationEdgeStyle;curved=1;rounded=0;html=1;endArrow=none;endFill=0;strokeWidth=1;strokeColor=#d6b656;" parent="1" source="4" target="11" edge="1">
          <mxGeometry relative="1" as="geometry" />
        </mxCell>
        <mxCell id="29" style="edgeStyle=entityRelationEdgeStyle;curved=1;rounded=0;html=1;endArrow=none;endFill=0;strokeWidth=1;strokeColor=#d6b656;" parent="1" source="4" target="12" edge="1">
          <mxGeometry relative="1" as="geometry" />
        </mxCell>
      </root>
    </mxGraphModel>
  </diagram>
</mxfile>
'@

# ── Validation Functions ──

function Test-ValidXml {
    param([string]$xml)
    try {
        [xml]$doc = $xml
        return 10
    } catch {
        return 0
    }
}

function Test-Hierarchy {
    param([xml]$doc)
    try {
        $mxfile = $doc.SelectSingleNode("//mxfile")
        $diagram = $doc.SelectSingleNode("//mxfile/diagram")
        $model = $doc.SelectSingleNode("//mxfile/diagram/mxGraphModel")
        $root = $doc.SelectSingleNode("//mxfile/diagram/mxGraphModel/root")
        if ($mxfile -and $diagram -and $model -and $root) { return 10 } else { return 0 }
    } catch { return 0 }
}

function Test-RootCells {
    param([xml]$doc)
    try {
        $cells = $doc.SelectNodes("//root/mxCell")
        $hasZero = $false
        $hasOne = $false
        foreach ($cell in $cells) {
            if ($cell.id -eq "0") { $hasZero = $true }
            if ($cell.id -eq "1" -and $cell.parent -eq "0") { $hasOne = $true }
        }
        if ($hasZero -and $hasOne) { return 10 } else { return 0 }
    } catch { return 0 }
}

function Test-SequentialIds {
    param([xml]$doc)
    try {
        $cells = $doc.SelectNodes("//root/mxCell")
        $ids = @()
        foreach ($cell in $cells) {
            $idStr = $cell.id
            $idNum = 0
            if ([int]::TryParse($idStr, [ref]$idNum)) {
                $ids += $idNum
            }
        }
        $sorted = $ids | Sort-Object
        # Check that IDs start at 0 and are mostly sequential
        if ($sorted[0] -eq 0 -and $sorted[1] -eq 1) {
            # Check for gaps - allow some gaps but penalize large ones
            $maxId = $sorted[-1]
            $expectedCount = $maxId + 1
            $actualCount = $sorted.Count
            if ($actualCount -ge ($expectedCount * 0.7)) { return 10 }
            else { return 5 }
        }
        return 3
    } catch { return 0 }
}

function Test-OfficialColors {
    param([xml]$doc)
    $allFills = @("#dae8fc", "#d5e8d4", "#fff2cc", "#ffe6cc", "#f8cecc", "#e1d5e7", "#b1ddf0",
                  "#ffffff", "#000000", "#f5f5f5", "#083F75", "#1061B0", "#23A2D9", "#8C8496",
                  "#6c8ebf", "#036897", "none")
    try {
        $cells = $doc.SelectNodes("//root/mxCell[@style]")
        $total = 0
        $valid = 0
        foreach ($cell in $cells) {
            $style = $cell.style
            if ($style -match "fillColor=(#[0-9a-fA-F]{6}|none)") {
                $total++
                $color = $matches[1].ToLower()
                if ($allFills -contains $color) { $valid++ }
            }
        }
        if ($total -eq 0) { return 10 }
        $ratio = $valid / $total
        return [math]::Round($ratio * 10)
    } catch { return 0 }
}

function Test-VertexEdgeAttributes {
    param([xml]$doc)
    try {
        $cells = $doc.SelectNodes("//root/mxCell[@style]")
        $total = 0
        $valid = 0
        foreach ($cell in $cells) {
            $style = $cell.style
            if (-not $style) { continue }
            $hasSource = $cell.HasAttribute("source") -or $cell.HasAttribute("target")
            $hasEdge = $cell.HasAttribute("edge")
            $hasVertex = $cell.HasAttribute("vertex")
            if ($hasSource -or $hasEdge) {
                $total++
                if ($cell.edge -eq "1") { $valid++ }
            } elseif ($hasVertex) {
                $total++
                if ($cell.vertex -eq "1") { $valid++ }
            }
        }
        if ($total -eq 0) { return 10 }
        $ratio = $valid / $total
        return [math]::Round($ratio * 10)
    } catch { return 0 }
}

function Test-ParentReferences {
    param([xml]$doc)
    try {
        $cells = $doc.SelectNodes("//root/mxCell")
        $ids = @{}
        foreach ($cell in $cells) { $ids[$cell.id] = $true }
        $total = 0
        $valid = 0
        foreach ($cell in $cells) {
            if ($cell.id -eq "0") { continue }
            $total++
            $parent = $cell.parent
            if ($parent -and $ids.ContainsKey($parent)) { $valid++ }
            elseif ($cell.id -eq "1" -and $parent -eq "0") { $valid++ }
        }
        if ($total -eq 0) { return 10 }
        $ratio = $valid / $total
        return [math]::Round($ratio * 10)
    } catch { return 0 }
}

function Test-Labels {
    param([xml]$doc)
    try {
        $cells = $doc.SelectNodes("//root/mxCell[@vertex='1']")
        $total = 0
        $labeled = 0
        foreach ($cell in $cells) {
            if ($cell.id -eq "0" -or $cell.id -eq "1") { continue }
            # Skip separator lines and activation boxes
            $style = $cell.style
            if ($style -match "^line;" -or $style -match "targetShapes=umlLifeline") { continue }
            $total++
            $val = $cell.value
            if ($val -and $val.Trim().Length -gt 0) { $labeled++ }
        }
        if ($total -eq 0) { return 10 }
        $ratio = $labeled / $total
        return [math]::Round($ratio * 10)
    } catch { return 0 }
}

function Test-GridAlignment {
    param([xml]$doc)
    try {
        $geos = $doc.SelectNodes("//mxGeometry[@as='geometry']")
        $total = 0
        $aligned = 0
        foreach ($geo in $geos) {
            $rel = $geo.GetAttribute("relative")
            if ($rel -eq "1") { continue }
            foreach ($attr in @("x", "y", "width", "height")) {
                $valStr = $geo.GetAttribute($attr)
                if (-not $valStr) { continue }
                $val = 0.0
                if ([double]::TryParse($valStr, [ref]$val)) {
                    $total++
                    if ($val % 10 -eq 0) { $aligned++ }
                }
            }
        }
        if ($total -eq 0) { return 10 }
        $ratio = $aligned / $total
        return [math]::Round($ratio * 10)
    } catch { return 0 }
}

function Test-Spacing {
    param([xml]$doc)
    try {
        # Collect all top-level vertex positions
        $positions = @()
        $cells = $doc.SelectNodes("//root/mxCell[@vertex='1' and @parent='1']")
        foreach ($cell in $cells) {
            $geo = $cell.SelectSingleNode("mxGeometry[@as='geometry']")
            if (-not $geo) { continue }
            $x = [double]$geo.GetAttribute("x")
            $y = [double]$geo.GetAttribute("y")
            $w = [double]$geo.GetAttribute("width")
            $h = [double]$geo.GetAttribute("height")
            $positions += @{ x=$x; y=$y; w=$w; h=$h }
        }
        if ($positions.Count -le 1) { return 10 }
        # Check that shapes don't overlap
        $overlaps = 0
        $pairs = 0
        for ($i = 0; $i -lt $positions.Count; $i++) {
            for ($j = $i+1; $j -lt $positions.Count; $j++) {
                $a = $positions[$i]
                $b = $positions[$j]
                $pairs++
                $hOverlap = ($a.x -lt ($b.x + $b.w)) -and (($a.x + $a.w) -gt $b.x)
                $vOverlap = ($a.y -lt ($b.y + $b.h)) -and (($a.y + $a.h) -gt $b.y)
                if ($hOverlap -and $vOverlap) { $overlaps++ }
            }
        }
        if ($pairs -eq 0) { return 10 }
        $ratio = 1 - ($overlaps / $pairs)
        return [math]::Round($ratio * 10)
    } catch { return 0 }
}

# ── Main Test Loop ──

$iterations = 40
$diagramTypes = @("flowchart", "class-diagram", "sequence-diagram", "er-diagram",
                  "network-diagram", "swimlane", "c4-context", "architecture",
                  "org-chart", "mind-map")

# Scores: hashtable of type -> array of scores
$allScores = @{}
foreach ($type in $diagramTypes) { $allScores[$type] = @() }

$criteriaNames = @(
    "Valid XML",
    "Hierarchy",
    "Root Cells",
    "Sequential IDs",
    "Official Colors",
    "Vertex/Edge Attrs",
    "Parent Refs",
    "Labels",
    "Grid Alignment",
    "Spacing"
)

# Per-criteria scores for detailed report
$criteriaScores = @{}
foreach ($type in $diagramTypes) {
    $criteriaScores[$type] = @{}
    foreach ($c in $criteriaNames) { $criteriaScores[$type][$c] = @() }
}

Write-Host "=========================================="
Write-Host "Draw.io Diagram Quality Test Harness"
Write-Host "=========================================="
Write-Host "Iterations: $iterations"
Write-Host "Diagram types: $($diagramTypes.Count)"
Write-Host ""

for ($iter = 1; $iter -le $iterations; $iter++) {
    Write-Host "Iteration $iter / $iterations" -NoNewline

    $iterDir = Join-Path $tempDir "iter-$iter"
    New-Item -ItemType Directory -Path $iterDir -Force | Out-Null

    foreach ($type in $diagramTypes) {
        $filePath = Join-Path $iterDir "$type.drawio"
        $xml = $templates[$type]
        Set-Content -Path $filePath -Value $xml -Encoding UTF8

        # Validate
        $score = 0
        $details = @()

        # 1. Valid XML
        $s1 = Test-ValidXml $xml
        $score += $s1
        $details += $s1
        $criteriaScores[$type]["Valid XML"] += $s1

        if ($s1 -gt 0) {
            [xml]$doc = $xml

            # 2. Hierarchy
            $s2 = Test-Hierarchy $doc
            $score += $s2; $details += $s2
            $criteriaScores[$type]["Hierarchy"] += $s2

            # 3. Root Cells
            $s3 = Test-RootCells $doc
            $score += $s3; $details += $s3
            $criteriaScores[$type]["Root Cells"] += $s3

            # 4. Sequential IDs
            $s4 = Test-SequentialIds $doc
            $score += $s4; $details += $s4
            $criteriaScores[$type]["Sequential IDs"] += $s4

            # 5. Official Colors
            $s5 = Test-OfficialColors $doc
            $score += $s5; $details += $s5
            $criteriaScores[$type]["Official Colors"] += $s5

            # 6. Vertex/Edge
            $s6 = Test-VertexEdgeAttributes $doc
            $score += $s6; $details += $s6
            $criteriaScores[$type]["Vertex/Edge Attrs"] += $s6

            # 7. Parent Refs
            $s7 = Test-ParentReferences $doc
            $score += $s7; $details += $s7
            $criteriaScores[$type]["Parent Refs"] += $s7

            # 8. Labels
            $s8 = Test-Labels $doc
            $score += $s8; $details += $s8
            $criteriaScores[$type]["Labels"] += $s8

            # 9. Grid Alignment
            $s9 = Test-GridAlignment $doc
            $score += $s9; $details += $s9
            $criteriaScores[$type]["Grid Alignment"] += $s9

            # 10. Spacing
            $s10 = Test-Spacing $doc
            $score += $s10; $details += $s10
            $criteriaScores[$type]["Spacing"] += $s10
        } else {
            foreach ($c in $criteriaNames[1..9]) {
                $criteriaScores[$type][$c] += 0
            }
        }

        $allScores[$type] += $score
    }
    Write-Host " - done (all types scored)"
}

# ── Compute Statistics ──

Write-Host ""
Write-Host "=========================================="
Write-Host "RESULTS SUMMARY"
Write-Host "=========================================="
Write-Host ""

$overallScores = @()

foreach ($type in $diagramTypes) {
    $scores = $allScores[$type]
    $mean = ($scores | Measure-Object -Average).Average
    $variance = 0
    foreach ($s in $scores) { $variance += [math]::Pow($s - $mean, 2) }
    $variance = $variance / $scores.Count
    $stddev = [math]::Sqrt($variance)
    $overallScores += $scores

    Write-Host ("{0,-22} Mean: {1,6:F2}  StdDev: {2,5:F2}  Min: {3,3}  Max: {4,3}" -f $type, $mean, $stddev, ($scores | Measure-Object -Minimum).Minimum, ($scores | Measure-Object -Maximum).Maximum)
}

Write-Host ""
$overallMean = ($overallScores | Measure-Object -Average).Average
$overallVariance = 0
foreach ($s in $overallScores) { $overallVariance += [math]::Pow($s - $overallMean, 2) }
$overallVariance = $overallVariance / $overallScores.Count
$overallStdDev = [math]::Sqrt($overallVariance)

Write-Host ("OVERALL                Mean: {0,6:F2}  StdDev: {1,5:F2}" -f $overallMean, $overallStdDev)
Write-Host ""

# Per-criteria breakdown
Write-Host "PER-CRITERIA BREAKDOWN (mean score out of 10):"
Write-Host ("{0,-22}" -f "Diagram Type") -NoNewline
foreach ($c in $criteriaNames) { Write-Host ("{0,8}" -f $c.Substring(0, [math]::Min(7, $c.Length))) -NoNewline }
Write-Host ""

foreach ($type in $diagramTypes) {
    Write-Host ("{0,-22}" -f $type) -NoNewline
    foreach ($c in $criteriaNames) {
        $cScores = $criteriaScores[$type][$c]
        $cMean = ($cScores | Measure-Object -Average).Average
        Write-Host ("{0,8:F1}" -f $cMean) -NoNewline
    }
    Write-Host ""
}

Write-Host ""
Write-Host "Test complete. $iterations iterations x $($diagramTypes.Count) diagrams = $($iterations * $diagramTypes.Count) total validations."
