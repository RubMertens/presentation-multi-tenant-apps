# Diagram Alternatives to Hand-Written SVGs

## Current State

The presentation (`slides-intro.html`) contains several inline SVG diagrams showing multi-tenancy
architecture patterns (spectrum overview, separate infrastructure, database per tenant, schema per
tenant, discriminator column). These SVGs are hand-written with hardcoded coordinates, colors, and
text positions -- typically 50-80 lines of SVG markup per diagram. Editing them requires manually
adjusting `x`, `y`, `width`, `height`, `viewBox` values, which is error-prone and time-consuming.

The presentation uses **Reveal.js 5** loaded from unpkg CDN, with the Info Support corporate theme
(primary: `#003865`, accent: `#00A3E0`, light blue: `#6ECFF6`).

---

## Option 1: Mermaid.js (with Reveal.js Plugin)

**What it is:** A text-based diagram DSL that renders diagrams in the browser using JavaScript.

**Reveal.js integration:** A dedicated plugin exists:
[`reveal.js-mermaid-plugin`](https://github.com/zjffun/reveal.js-mermaid-plugin) (49 stars, actively
maintained). Usage involves adding a `<script>` tag for the plugin + mermaid library, then writing
diagrams inline:

```html
<div class="mermaid">
  <pre>
    flowchart LR
      A[Tenant A] --> App[Shared Application]
      B[Tenant B] --> App
      App --> DB_A[(DB A)]
      App --> DB_B[(DB B)]
  </pre>
</div>
```

Configuration in `Reveal.initialize()`:
```js
plugins: [RevealNotes, RevealHighlight, RevealMermaid],
mermaid: { theme: 'base', themeVariables: { primaryColor: '#003865' } }
```

| Criterion | Rating | Notes |
|-----------|--------|-------|
| Ease of editing | Excellent | Plain text, no coordinates. Anyone can modify. |
| Reveal.js integration | Good | Plugin exists, works with Reveal.js 5, CDN available. |
| Visual quality | Good | Clean, professional. Supports theming with custom colors. |
| Workflow | Excellent | Edit text in HTML -> refresh browser -> done. No build step. |

**Limitations:**
- Layout is automatic -- you have less control over exact positioning than raw SVG.
- Architecture diagrams (boxes with sub-components) require creative use of `flowchart` or `block`
  diagram types. The `architecture` diagram type is experimental (added in Mermaid v11).
- Theming to match Info Support brand requires `themeVariables` config, which covers primary colors
  but not every element.

**Best for:** Quick, maintainable diagrams where exact pixel control isn't critical.

---

## Option 2: Draw.io / diagrams.net

**What it is:** A visual drag-and-drop diagram editor (free, open source). Available as a web app
at diagrams.net and as a VS Code extension (`hediet.vscode-drawio`).

**Reveal.js integration:** Export diagrams as SVG or PNG files, then embed via `<img>` tags:
```html
<img src="diagrams/multi-tenancy-spectrum.svg" alt="Multi-tenancy spectrum" />
```

| Criterion | Rating | Notes |
|-----------|--------|-------|
| Ease of editing | Excellent | Visual editor, drag and drop. Non-technical users can edit. |
| Reveal.js integration | Good | Export SVG/PNG, embed as images. Simple and reliable. |
| Visual quality | Excellent | Full control over styling. Can match any brand exactly. |
| Workflow | Moderate | Edit in Draw.io -> export SVG/PNG -> replace file -> refresh. |

**Strengths:**
- VS Code extension lets you edit `.drawio` files without leaving the editor.
- `.drawio` files are XML, so they can be version-controlled in git.
- Huge shape library including cloud architecture, network, and infrastructure icons.
- Can set exact Info Support brand colors (`#003865`, `#00A3E0`, etc.) per element.

**Limitations:**
- Exported SVGs may be larger/more complex than hand-written ones (though browsers handle this fine).
- Requires a manual export step when diagrams change.
- Two files to maintain per diagram: the `.drawio` source and the exported `.svg`/`.png`.

**Best for:** Complex architecture diagrams requiring precise visual control and brand compliance.

---

## Option 3: Excalidraw

**What it is:** A collaborative drawing tool with a distinctive hand-drawn/sketchy aesthetic.
Available as a web app and a VS Code extension.

**Reveal.js integration:** Same as Draw.io -- export SVG/PNG, embed as images.

| Criterion | Rating | Notes |
|-----------|--------|-------|
| Ease of editing | Excellent | Intuitive whiteboard-style interface. |
| Reveal.js integration | Good | Export and embed as images. |
| Visual quality | Moderate | Hand-drawn style may be too informal for a corporate presentation. |
| Workflow | Moderate | Edit -> export -> replace file -> refresh. |

**Limitations:**
- The hand-drawn aesthetic is its defining feature, but may not suit Info Support's corporate
  visual style (clean, professional, structured).
- Less control over precise alignment compared to Draw.io.
- Theming/brand colors require manual color picking per element.

**Best for:** Informal presentations, workshops, or whiteboard-style sessions. Less ideal for
polished corporate talks.

---

## Option 4: D2

**What it is:** A modern text-based diagram scripting language by Terrastruct. Compiles `.d2` files
to SVG, PNG, or PDF.

**Example syntax:**
```d2
tenantA: Tenant A {
  app: App Server
  db: Database
}
tenantB: Tenant B {
  app: App Server
  db: Database
}
```

**Reveal.js integration:** D2 is a CLI tool -- you run `d2 diagram.d2 diagram.svg` to generate
output, then embed the SVG as an image. No browser-side rendering.

| Criterion | Rating | Notes |
|-----------|--------|-------|
| Ease of editing | Good | Clean text syntax, easier than SVG. Steeper learning curve than Mermaid. |
| Reveal.js integration | Moderate | Requires build step (CLI). No browser plugin. |
| Visual quality | Excellent | Clean, modern output. Multiple layout engines including one designed for architecture diagrams (TALA). |
| Workflow | Moderate | Edit `.d2` file -> run CLI -> embed SVG -> refresh. Can use `d2 --watch` for live preview. |

**Strengths:**
- The TALA layout engine is specifically designed for software architecture diagrams.
- Supports nested containers, which maps well to the multi-tenancy diagrams.
- Very clean, professional output.

**Limitations:**
- Requires installing the `d2` CLI tool (not browser-based).
- Extra build step adds friction compared to Mermaid's in-browser rendering.
- Smaller community and ecosystem than Mermaid.
- Custom theming/colors requires a theme file or inline style overrides.

**Best for:** Users who want text-based diagrams with higher visual quality than Mermaid and are
comfortable with a build step.

---

## Option 5: PlantUML

**What it is:** A mature text-based diagram tool, primarily designed for UML diagrams but also
supports deployment and component diagrams.

**Reveal.js integration:** Requires a Java-based server or CLI to render. Output is PNG or SVG,
embedded as images.

| Criterion | Rating | Notes |
|-----------|--------|-------|
| Ease of editing | Moderate | Text-based but verbose syntax. UML-centric vocabulary. |
| Reveal.js integration | Poor | Requires Java runtime or server. Heavy toolchain. |
| Visual quality | Moderate | Functional but dated-looking output compared to modern tools. |
| Workflow | Poor | Edit -> run Java CLI/server -> export -> embed. Heaviest toolchain. |

**Limitations:**
- Java dependency is a significant friction point.
- Output style looks dated compared to Mermaid or D2.
- Architecture diagrams are possible but the UML-centric syntax feels awkward for
  infrastructure/multi-tenancy diagrams.

**Not recommended** for this use case.

---

## Option 6: Static Image Approach (PNG/SVG from Any Tool)

**What it is:** Use any visual tool (PowerPoint, Figma, Canva, Keynote, etc.) to create diagrams,
export as PNG or SVG, and embed as `<img>` tags.

| Criterion | Rating | Notes |
|-----------|--------|-------|
| Ease of editing | Varies | Depends on the tool. PowerPoint/Figma are very accessible. |
| Reveal.js integration | Excellent | Just `<img src="...">`. Simplest possible integration. |
| Visual quality | Varies | Depends entirely on the tool and designer skill. |
| Workflow | Moderate | Edit in external tool -> export -> replace file -> refresh. |

**Strengths:**
- Maximum flexibility -- use whatever tool you're most comfortable with.
- No new tools to learn.
- The existing PowerPoint version of this presentation may already have diagrams that can be exported.

**Limitations:**
- Source files may not be easily version-controlled (e.g., `.pptx`, `.fig`).
- No text-searchability or accessibility in the final output (unless SVG with text elements).

---

## Comparison Matrix

| Tool | Ease of Edit | Reveal.js Integration | Visual Quality | Workflow Simplicity | Brand Control |
|------|-------------|----------------------|----------------|-------------------|---------------|
| **Mermaid.js** | 5/5 | 4/5 (plugin) | 3.5/5 | 5/5 | 3/5 |
| **Draw.io** | 5/5 | 4/5 (image embed) | 5/5 | 3/5 | 5/5 |
| **Excalidraw** | 5/5 | 4/5 (image embed) | 2.5/5 | 3/5 | 2/5 |
| **D2** | 3.5/5 | 3/5 (CLI build) | 4.5/5 | 3/5 | 3.5/5 |
| **PlantUML** | 2.5/5 | 2/5 (Java dep) | 2.5/5 | 2/5 | 2/5 |
| **Static Images** | 4/5 | 5/5 | 4/5 | 3/5 | 5/5 |

---

## Recommendations

### Primary Recommendation: Mermaid.js with Reveal.js Plugin

**Why:** For this specific use case (architecture diagrams in a Reveal.js presentation), Mermaid
offers the best balance of ease-of-use and integration:

- **Zero build step:** Diagrams render directly in the browser. Edit text, refresh, done.
- **Plugin exists for Reveal.js 5:** Add one `<script>` tag and register the plugin.
- **Diagrams are inline in the HTML:** No separate files to manage or export steps.
- **Theming supports custom colors:** Can configure Info Support brand colors via `themeVariables`.
- **Git-friendly:** Diagram changes show as readable text diffs.
- **Low learning curve:** The flowchart/block syntax is intuitive.

The multi-tenancy spectrum and approach diagrams can all be expressed as Mermaid flowcharts or
block diagrams. The output won't be pixel-identical to the current hand-crafted SVGs, but it will
be clean, professional, and dramatically easier to maintain.

**Setup required:**
1. Add mermaid + plugin scripts to `index.html` (CDN, no npm needed).
2. Register `RevealMermaid` in the plugins array.
3. Replace inline `<svg>` blocks with `<div class="mermaid"><pre>...</pre></div>`.

### Secondary Recommendation: Draw.io (for diagrams needing precise control)

**Why:** If any diagram requires exact visual positioning, custom iconography, or pixel-perfect
brand compliance that Mermaid's automatic layout can't achieve, Draw.io is the best fallback:

- VS Code extension makes editing seamless (no browser tab switching).
- `.drawio` source files are XML and version-control well.
- Export to SVG gives crisp, scalable output.
- Full control over every visual element.

**Suggested hybrid approach:** Use Mermaid for most diagrams (quick to edit), and Draw.io for the
one or two diagrams that need very specific visual layouts (e.g., the detailed infrastructure
comparison with the gradient spectrum bar).
