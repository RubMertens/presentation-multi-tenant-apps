# Mermaid.js Styling Guide for Info Support Presentation

## Brand Colors Reference

| Token            | Hex       | Usage                              |
|------------------|-----------|------------------------------------|
| Dark Blue        | `#003865` | Headings, node borders, dark accents |
| Sky Blue         | `#00A3E0` | Links, edges, secondary elements   |
| Light Blue       | `#6ECFF6` | Highlights, hover states           |
| Green Accent     | `#BED62F` | Accent bar, call-to-action         |
| Body Text        | `#355670` | Regular text                       |
| White            | `#ffffff` | Backgrounds                        |
| Light Gray       | `#F1F1F1` | Subtle backgrounds                 |
| Gray             | `#D9D9D9` | Borders, dividers                  |

---

## 1. Mermaid Theme System

### Available Themes

Mermaid ships with five built-in themes:

| Theme       | Notes                                                |
|-------------|------------------------------------------------------|
| `default`   | Standard theme, not customizable via themeVariables  |
| `neutral`   | Black-and-white, good for print                      |
| `dark`      | Dark mode                                            |
| `forest`    | Green palette                                        |
| **`base`**  | **Only theme that supports full themeVariables customization** |

**Always use `theme: 'base'`** when customizing. Other themes ignore most themeVariables.

### How themeVariables Work

The base theme uses a cascading calculation system. Setting `primaryColor` causes Mermaid to automatically derive:
- `primaryBorderColor` (darkened version of primaryColor)
- `primaryTextColor` (contrast color based on primaryColor lightness)
- `nodeBkg` (defaults to primaryColor)
- `mainBkg` (defaults to primaryColor)

This means setting `primaryColor: '#003865'` (dark blue) makes node backgrounds dark blue with white text -- which is why the current config produces dark nodes. If we want light/white node backgrounds with dark blue borders, we need to override more variables.

**Important**: Mermaid only recognizes hex color codes, not CSS color names.

---

## 2. Complete themeVariables Reference (Flowchart-Relevant)

### Core Colors

| Variable             | Default (base)        | Purpose                                    |
|----------------------|-----------------------|--------------------------------------------|
| `primaryColor`       | `#fff4dd` (beige)     | Main node background; cascades to many others |
| `secondaryColor`     | derived from primary  | Secondary node backgrounds, edge labels    |
| `tertiaryColor`      | derived from primary  | **Cluster/subgraph backgrounds** (the brown culprit!) |
| `background`         | `#f4f4f4`             | Overall diagram background                 |

### Text Colors

| Variable             | Default               | Purpose                                    |
|----------------------|-----------------------|--------------------------------------------|
| `primaryTextColor`   | `#333` or `#eee`      | Text inside primary-colored nodes          |
| `secondaryTextColor` | inverted secondary    | Text inside secondary elements             |
| `tertiaryTextColor`  | inverted tertiary     | Cluster/subgraph label text                |
| `textColor`          | = primaryTextColor    | General text fallback                      |

### Border Colors

| Variable               | Default             | Purpose                                  |
|------------------------|---------------------|------------------------------------------|
| `primaryBorderColor`   | derived from primary | Node border color                        |
| `secondaryBorderColor` | derived from secondary | Secondary element borders              |
| `tertiaryBorderColor`  | derived from tertiary | **Cluster/subgraph border color**       |

### Flowchart-Specific Variables

| Variable             | Default               | Purpose                                    |
|----------------------|-----------------------|--------------------------------------------|
| `nodeBkg`            | = primaryColor        | Node rectangle fill                        |
| `mainBkg`            | = primaryColor        | Main background of nodes                   |
| `nodeBorder`         | = primaryBorderColor  | Node rectangle stroke                      |
| `nodeTextColor`      | = primaryTextColor    | Text inside nodes                          |
| `clusterBkg`         | = tertiaryColor       | **Subgraph/cluster background fill**       |
| `clusterBorder`      | = tertiaryBorderColor | Subgraph/cluster border stroke             |
| `titleColor`         | = tertiaryTextColor   | Diagram title text color                   |

### Edge/Line Variables

| Variable             | Default               | Purpose                                    |
|----------------------|-----------------------|--------------------------------------------|
| `lineColor`          | inverted background   | Edge/connection line color                 |
| `arrowheadColor`     | inverted background   | Arrowhead fill                             |
| `defaultLinkColor`   | = lineColor           | Edge path color                            |
| `edgeLabelBackground`| = secondaryColor      | Background behind edge label text          |

### Note Variables

| Variable             | Default               | Purpose                                    |
|----------------------|-----------------------|--------------------------------------------|
| `noteBkgColor`       | `#fff5ad`             | Note box background                        |
| `noteTextColor`      | `#333`                | Note text color                            |
| `noteBorderColor`    | derived from noteBkg  | Note box border                            |

---

## 3. The Brown/Beige Problem

The default `base` theme has `primaryColor: '#fff4dd'` (beige/cream). The `tertiaryColor` is automatically calculated from this with a hue shift, producing a brownish/olive tone. Since:

- `clusterBkg` = `tertiaryColor`
- `clusterBorder` = `tertiaryBorderColor`

...all subgraphs get an ugly brown/beige background. The current config sets `primaryColor: '#003865'` which makes nodes dark blue (fine for some elements) but the derived tertiaryColor still produces muddy cluster backgrounds.

**Fix**: Explicitly set `tertiaryColor`, `clusterBkg`, and `clusterBorder` to override the cascade.

---

## 4. CSS Selectors for Mermaid SVG Elements

Mermaid renders diagrams as inline SVGs. The following CSS selectors can be used to override styles:

### Node Elements
```css
.mermaid .node rect         /* Rectangle nodes */
.mermaid .node circle       /* Circle nodes */
.mermaid .node ellipse      /* Ellipse nodes */
.mermaid .node polygon      /* Diamond/rhombus nodes */
.mermaid .node path         /* Custom path nodes */
.mermaid .node .label       /* Label container inside node */
.mermaid .node .label text  /* Actual text in node label */
```

### Cluster/Subgraph Elements
```css
.mermaid .cluster rect       /* Subgraph background rectangle */
.mermaid .cluster text       /* Subgraph label text */
.mermaid .cluster span       /* Subgraph label span (HTML labels) */
.mermaid .cluster-label text /* Alternative cluster label selector */
```

### Edge/Connection Elements
```css
.mermaid .edgePath .path     /* Edge line path */
.mermaid .flowchart-link     /* Alternative edge selector */
.mermaid .arrowheadPath      /* Arrowhead fill */
```

### Edge Labels
```css
.mermaid .edgeLabel          /* Edge label container */
.mermaid .edgeLabel rect     /* Edge label background */
.mermaid .edgeLabel p        /* Edge label text paragraph */
.mermaid .labelBkg           /* Label background rectangle */
```

### Other
```css
.mermaid .flowchartTitleText /* Diagram title */
div.mermaidTooltip           /* Tooltip popup */
```

### CSS Override Limitations

- Mermaid applies inline `style` attributes on many elements, which have higher specificity than external CSS
- Use `!important` sparingly to override inline styles
- themeVariables is the preferred approach; CSS is a fallback for fine-tuning
- The `.mermaid` wrapper class scopes all rules to Mermaid diagrams

---

## 5. Per-Diagram Styling

### Method 1: Frontmatter Config (Mermaid v10+)
```
---
config:
  theme: base
  themeVariables:
    primaryColor: '#e8f4fd'
---
graph TD
  A --> B
```

### Method 2: Init Directive (Older / Plugin-Compatible)
```
%%{init: {'theme': 'base', 'themeVariables': {'primaryColor': '#e8f4fd'}}}%%
graph TD
  A --> B
```

### Method 3: Inline Style Directives
```
graph TD
  A[Node A] --> B[Node B]
  style A fill:#e8f4fd,stroke:#003865,color:#003865
  style B fill:#ffffff,stroke:#00A3E0,color:#355670
```

### Method 4: CSS Classes in Mermaid
```
graph TD
  A[Node A]:::blueNode --> B[Node B]:::whiteNode
  classDef blueNode fill:#e8f4fd,stroke:#003865,color:#003865
  classDef whiteNode fill:#ffffff,stroke:#003865,color:#355670
```

For this presentation, **Method 1/2 (global config via Reveal.initialize)** is best for consistency, with **Method 3/4** for individual node overrides (like the warning node in the discriminator slide).

---

## 6. Concrete Recommendation: Info Support Mermaid Theme

### Recommended themeVariables Config

Replace the current mermaid config in `presentation/index.html`:

```javascript
mermaid: {
  theme: 'base',
  themeVariables: {
    // Node backgrounds: light blue-tinted white
    primaryColor: '#e8f4fd',
    // Secondary: very light gray for edge labels
    secondaryColor: '#f0f4f8',
    // Tertiary: light blue-gray for subgraph/cluster backgrounds
    tertiaryColor: '#eaf2f8',

    // Text colors
    primaryTextColor: '#003865',
    secondaryTextColor: '#355670',
    tertiaryTextColor: '#003865',
    textColor: '#355670',

    // Border colors
    primaryBorderColor: '#003865',
    secondaryBorderColor: '#00A3E0',
    tertiaryBorderColor: '#6ECFF6',

    // Edge/line colors
    lineColor: '#00A3E0',
    arrowheadColor: '#003865',

    // Flowchart overrides
    nodeBkg: '#e8f4fd',
    mainBkg: '#e8f4fd',
    nodeBorder: '#003865',
    nodeTextColor: '#003865',

    // Cluster/subgraph: clean light background, no brown
    clusterBkg: '#f0f6fc',
    clusterBorder: '#6ECFF6',

    // Edge labels
    edgeLabelBackground: '#ffffff',

    // Notes
    noteBkgColor: '#f0f6fc',
    noteTextColor: '#355670',
    noteBorderColor: '#6ECFF6',

    // Title
    titleColor: '#003865',

    // Background
    background: '#ffffff',

    // Fonts
    fontFamily: '"GraphikRegular", "Segoe UI", system-ui, sans-serif',
    fontSize: '14px',
  }
}
```

### Design Rationale

| Element         | Color         | Why                                              |
|-----------------|---------------|--------------------------------------------------|
| Node fill       | `#e8f4fd`     | Very light blue tint -- clean, corporate, not white-on-white |
| Node border     | `#003865`     | Dark blue brand color -- crisp definition         |
| Node text       | `#003865`     | Dark blue -- high contrast, brand-consistent      |
| Cluster fill    | `#f0f6fc`     | Slightly different light blue -- distinguishes groups without brown |
| Cluster border  | `#6ECFF6`     | Light blue -- softer than node borders, establishes hierarchy |
| Edge lines      | `#00A3E0`     | Sky blue -- brand color, clearly visible          |
| Arrowheads      | `#003865`     | Dark blue -- sharp, definitive direction          |
| Edge labels     | `#ffffff`     | White background -- clean readability             |
| Background      | `#ffffff`     | White -- matches slide background                 |

### CSS Overrides for infosupport-theme.css

Add these rules to `presentation/css/infosupport-theme.css` to fine-tune what themeVariables cannot reach:

```css
/* ============================================================
   Mermaid Diagram Overrides
   ============================================================ */

/* Ensure cluster/subgraph backgrounds are clean */
.reveal .mermaid .cluster rect {
  rx: 8px !important;
  ry: 8px !important;
  stroke-width: 1.5px !important;
}

/* Node rectangles: rounded corners for modern look */
.reveal .mermaid .node rect,
.reveal .mermaid .node circle,
.reveal .mermaid .node polygon {
  rx: 6px !important;
  ry: 6px !important;
  stroke-width: 1.5px !important;
}

/* Edge paths: consistent width */
.reveal .mermaid .edgePath .path {
  stroke-width: 2px !important;
}

/* Arrowheads */
.reveal .mermaid .arrowheadPath {
  fill: #003865 !important;
}

/* Edge labels: clean white background */
.reveal .mermaid .edgeLabel {
  background-color: #ffffff !important;
}

.reveal .mermaid .edgeLabel rect {
  fill: #ffffff !important;
  opacity: 0.9 !important;
}

/* Node text: ensure brand font */
.reveal .mermaid .node .label text,
.reveal .mermaid .cluster-label text {
  font-family: "GraphikRegular", "Segoe UI", system-ui, sans-serif !important;
  fill: #003865 !important;
}

/* Cluster label: bold for subgraph titles */
.reveal .mermaid .cluster-label text {
  font-weight: 700 !important;
  font-size: 14px !important;
}

/* Flowchart link text */
.reveal .mermaid .edgeLabel p {
  color: #355670 !important;
  font-family: "GraphikRegular", "Segoe UI", system-ui, sans-serif !important;
}

/* Database cylinder nodes: same styling */
.reveal .mermaid .node path {
  stroke-width: 1.5px !important;
}

/* Scale Mermaid diagrams nicely within slides */
.reveal .mermaid svg {
  max-width: 100%;
  max-height: 500px;
}

/* Dark slide overrides: when Mermaid is on a dark background */
.reveal .slides section.section-header .mermaid .node .label text,
.reveal .slides section.title-slide .mermaid .node .label text {
  fill: #ffffff !important;
}
```

### Example Per-Diagram Override (for special nodes)

The discriminator slide has a warning node that should stay orange/red. Use inline Mermaid `style` directives for these one-offs:

```
style WARN fill:#fce4ec,stroke:#e74c3c,color:#c0392b
style T fill:#fff3e0,stroke:#e67e22
```

These inline styles override the global theme for specific nodes, which is the correct approach for emphasis elements. The existing styles in the presentation for these elements are already correct.

---

## 7. Best Practices for Professional Mermaid Diagrams

1. **Use the `base` theme with explicit overrides** -- never rely on auto-derived colors for presentation-quality output.

2. **Keep node text short** -- long labels cause layout issues. Use `\n` for line breaks.

3. **Limit subgraph nesting** -- max 2 levels deep. Deeply nested subgraphs become unreadable.

4. **Use consistent node shapes** -- rectangles for services/apps, cylinders `[( )]` for databases, diamonds `{ }` for decisions.

5. **Set `fontFamily`** in themeVariables to match presentation fonts for visual cohesion.

6. **Avoid too many edges** -- if a diagram has more than ~15 nodes, split it into multiple diagrams.

7. **Use `classDef` for reusable styles** within a single diagram rather than repeating `style` directives.

8. **Test at presentation resolution** -- Mermaid SVGs can look different at different viewport sizes. The `max-height` CSS rule prevents overflow.

9. **Prefer horizontal (`LR`) for pipelines/flows**, vertical (`TD`) for hierarchies/layers.

10. **White/light backgrounds** always look more professional than colored backgrounds for corporate presentations. Reserve color for borders and accents.
