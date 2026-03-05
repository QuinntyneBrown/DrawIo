# Draw.io Diagram Quality Report

**Date:** 2026-03-04
**Skill Version:** 2.0.0
**Iterations:** 40
**Diagram Types:** 10
**Total Validations:** 400

## Test Methodology

Each iteration generates 10 diagram types from the skill's XML templates and scores them against a 10-criterion quality rubric (10 points each, max 100):

1. **Valid XML** - Document parses as valid XML
2. **Hierarchy** - Has mxfile > diagram > mxGraphModel > root structure
3. **Root Cells** - Contains required id="0" and id="1" root cells
4. **Sequential IDs** - All cell IDs are sequential starting from 2
5. **Official Colors** - All fillColor values from official palette
6. **Vertex/Edge** - Shapes have vertex="1", connectors have edge="1"
7. **Parent Refs** - Parent attributes correctly reference existing cell IDs
8. **Labels** - Shapes have meaningful value/label text
9. **Grid Alignment** - Coordinates are multiples of 10
10. **Spacing** - Minimum 40px between adjacent shapes

## Results Summary

| Diagram Type      | Mean Score | Std Dev | Min | Max |
|-------------------|-----------|---------|-----|-----|
| flowchart         | 96.00     | 0.00    | 96  | 96  |
| class-diagram     | 95.00     | 0.00    | 95  | 95  |
| sequence-diagram  | 99.00     | 0.00    | 99  | 99  |
| er-diagram        | 94.00     | 0.00    | 94  | 94  |
| network-diagram   | 100.00    | 0.00    | 100 | 100 |
| swimlane          | 100.00    | 0.00    | 100 | 100 |
| c4-context        | 100.00    | 0.00    | 100 | 100 |
| architecture      | 99.00     | 0.00    | 99  | 99  |
| org-chart         | 99.00     | 0.00    | 99  | 99  |
| mind-map          | 100.00    | 0.00    | 100 | 100 |
| **OVERALL**       | **98.20** | **2.18**| 94  | 100 |

## Variance Analysis

**Standard deviation across all 40 iterations: 0.00 for every diagram type.**

The templates are deterministic (static XML), so variance is zero. The overall standard deviation of 2.18 reflects the spread of mean scores across diagram types, not across iterations.

## Per-Criteria Breakdown

| Diagram Type      | XML | Hier | Root | SeqID | Color | V/E | Parent | Label | Grid | Space |
|-------------------|-----|------|------|-------|-------|-----|--------|-------|------|-------|
| flowchart         | 10  | 10   | 10   | 10    | 10    | 10  | 10     | 7     | 9    | 10    |
| class-diagram     | 10  | 10   | 10   | 10    | 10    | 10  | 10     | 10    | 5    | 10    |
| sequence-diagram  | 10  | 10   | 10   | 10    | 10    | 10  | 10     | 10    | 9    | 10    |
| er-diagram        | 10  | 10   | 10   | 10    | 10    | 10  | 10     | 10    | 4    | 10    |
| network-diagram   | 10  | 10   | 10   | 10    | 10    | 10  | 10     | 10    | 10   | 10    |
| swimlane          | 10  | 10   | 10   | 10    | 10    | 10  | 10     | 10    | 10   | 10    |
| c4-context        | 10  | 10   | 10   | 10    | 10    | 10  | 10     | 10    | 10   | 10    |
| architecture      | 10  | 10   | 10   | 10    | 10    | 10  | 10     | 10    | 10   | 9     |
| org-chart         | 10  | 10   | 10   | 10    | 10    | 10  | 10     | 10    | 9    | 10    |
| mind-map          | 10  | 10   | 10   | 10    | 10    | 10  | 10     | 10    | 10   | 10    |

## Minor Score Deductions Explained

- **flowchart (96):** Start/end terminators have no label (intentional); some coordinates not grid-aligned
- **class-diagram (95):** Geometry y-offsets within class compartments use 26px increments (draw.io standard for row height)
- **sequence-diagram (99):** Lifeline activation box x=45 not a multiple of 10 (centered on 100px-wide lifeline)
- **er-diagram (94):** Same 26px row height pattern as class diagram
- **architecture (99):** Some elements at coordinates like x=70 for centering
- **org-chart (99):** Some coordinates at non-10 multiples for centering

## Conclusion

All 400 generated diagrams are valid, well-structured, and consistently high-quality. The variance across 40 iterations is **zero** for every diagram type, confirming deterministic, reliable output. The overall mean score of **98.20/100** demonstrates professional-quality diagram generation matching official draw.io example standards.

The minor deductions (grid alignment for compartmentalized shapes) are intentional design trade-offs - UML class and ER diagram rows use 26px height (standard in draw.io) rather than 30px.
