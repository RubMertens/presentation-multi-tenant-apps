# Reveal.js Plugin Recommendations

Recommendations for the "Building Multi-Tenant Applications" presentation.

**Current setup:** Reveal.js 5.x loaded via unpkg CDN, with `RevealNotes` and `RevealHighlight` plugins. 11 chapters, ~60 slides, heavy use of inline SVG diagrams and `<pre><code>` blocks (C#, JSON, Razor, bash).

---

## Highly Recommended

### 1. reveal.js-mermaid-plugin

- **What:** Renders Mermaid diagram markup directly in the browser. Supports flowcharts, sequence diagrams, class diagrams, state diagrams, ER diagrams, and more.
- **Why for this presentation:** The current slides contain ~10 hand-coded inline SVG diagrams (multi-tenancy spectrum, tenant resolution flow, database-per-tenant architecture, etc.). Replacing these with Mermaid markup would make them dramatically easier to maintain, modify, and animate. Mermaid's flowchart and architecture diagram types map well to tenancy model visualizations.
- **Install (CDN):**
  ```html
  <script src="https://cdn.jsdelivr.net/npm/reveal.js-mermaid-plugin/plugin/mermaid/mermaid.js"></script>
  ```
  Then add `RevealMermaid` to the `plugins` array.
- **Usage:**
  ```html
  <section>
    <div class="mermaid">
      graph TD
        A[Request] --> B{Tenant Resolver}
        B --> C[Host-based]
        B --> D[Header-based]
        B --> E[Path-based]
    </div>
  </section>
  ```
- **npm:** `npm install reveal.js-mermaid-plugin`
- **Repo:** https://www.npmjs.com/package/reveal.js-mermaid-plugin
- **Caveats:** Requires Mermaid.js (bundled by the plugin). Works with Reveal.js 5.x. Diagram styling can be customized via Mermaid theme config to match Info Support branding colors (#003865, #e87722).

---

### 2. reveal.js-simplemenu (Martino Magnifico)

- **What:** Generates a navigation menubar/header/footer from section titles. Automatically highlights the current chapter.
- **Why for this presentation:** With 11 chapters and ~60 slides, audiences easily lose context about where they are in the talk. A persistent chapter nav bar (e.g., in the footer) gives attendees a visual map of the presentation structure.
- **Install (CDN):**
  ```html
  <link rel="stylesheet" href="https://cdn.jsdelivr.net/npm/reveal.js-simplemenu/plugin/simplemenu/simplemenu.css" />
  <script src="https://cdn.jsdelivr.net/npm/reveal.js-simplemenu/plugin/simplemenu/simplemenu.js"></script>
  ```
  Then add `Simplemenu` to the `plugins` array.
- **Repo:** https://github.com/martinomagnifico/reveal.js-simplemenu
- **Demo:** https://martinomagnifico.github.io/reveal.js-simplemenu/demo.html
- **Caveats:** Fully compatible with Reveal.js 5.x. Requires `data-name` attributes on sections. CSS needs customization to match Info Support theme colors. Works alongside the existing slide number display.

---

### 3. reveal.js-copycode (Martino Magnifico)

- **What:** Automatically adds a "Copy" button to all code blocks.
- **Why for this presentation:** The presentation is code-heavy (C# middleware, EF Core configs, JSON settings, Razor pages, bash commands). Conference attendees following along or reviewing later will want to copy code snippets. This is a small but high-value UX improvement.
- **Install (CDN):**
  ```html
  <link rel="stylesheet" href="https://cdn.jsdelivr.net/npm/reveal.js-copycode/plugin/copycode/copycode.css" />
  <script src="https://cdn.jsdelivr.net/npm/reveal.js-copycode/plugin/copycode/copycode.js"></script>
  ```
  Then add `CopyCode` to the `plugins` array.
- **Repo:** https://github.com/Martinomagnifico/reveal.js-copycode
- **Demo:** https://martinomagnifico.github.io/reveal.js-copycode/demo/demo.html
- **Caveats:** Works with Reveal.js 5.x. Styling is minimal and easy to match to any theme. Button appears on hover, so it does not clutter the slide during presentation.

---

### 4. reveal.js-appearance (Martino Magnifico)

- **What:** PowerPoint-style sequential element animations using Animate.css. Elements can fade in, slide in, zoom, bounce, etc.
- **Why for this presentation:** Several slides build up concepts incrementally (the multi-tenancy spectrum, pros/cons lists, architecture layers). This plugin enables smooth entrance animations tied to fragments, making builds feel more polished than the default fragment fade-in.
- **Install (CDN):**
  ```html
  <link rel="stylesheet" href="https://cdn.jsdelivr.net/npm/reveal.js-appearance/plugin/appearance/appearance.css" />
  <script src="https://cdn.jsdelivr.net/npm/reveal.js-appearance/plugin/appearance/appearance.js"></script>
  ```
  Then add `Appearance` to the `plugins` array.
- **Repo:** https://github.com/martinomagnifico/reveal.js-appearance
- **Demo:** https://martinomagnifico.github.io/reveal.js-appearance/demo/demo.html
- **Caveats:** Requires Animate.css (can be loaded via CDN). Works with Reveal.js 5.x. Overuse can be distracting -- best reserved for key architectural diagrams and concept builds.

---

## Nice to Have

### 5. Spotlight

- **What:** Highlights the current mouse position with a spotlight/laser pointer effect, toggled by a keyboard shortcut.
- **Why for this presentation:** Useful during live demos when pointing at specific lines of code or parts of architecture diagrams. More visible than a regular mouse cursor on a projector.
- **Install (CDN):**
  ```html
  <script src="https://cdn.jsdelivr.net/npm/reveal.js-plugin-spotlight/spotlight.js"></script>
  ```
- **Repo:** https://github.com/denniskniep/reveal.js-plugin-spotlight
- **Caveats:** Simple plugin with no dependencies. Toggle with a shortcut key. Test with your presenter remote.

### 6. elapsed-time-bar

- **What:** Adds a progress bar showing elapsed time at the top/bottom of the presentation.
- **Why for this presentation:** Conference talks have strict time slots. A visible (to the speaker) elapsed-time bar helps with pacing across 60 slides.
- **Install (CDN):**
  ```html
  <script src="https://cdn.jsdelivr.net/npm/reveal.js-elapsed-time-bar/elapsed-time-bar.js"></script>
  ```
  Configure: `elapsedTimebar: { allottedTime: 45 * 60 * 1000 }` (45 minutes in ms)
- **Repo:** https://github.com/tkrkt/reveal.js-elapsed-time-bar
- **Demo:** https://tkrkt.github.io/reveal.js-elapsed-time-bar/
- **Caveats:** May need minor CSS tweaks for Reveal.js 5.x compatibility. Test before the talk.

### 7. reveal.js-pointer

- **What:** Changes the mouse cursor into a large colored dot (pointer) when a key is held.
- **Why for this presentation:** Simpler alternative to Spotlight for pointing at code during live presentation. No extra visual effects.
- **Repo:** https://github.com/burnpiro/reveal-pointer
- **Caveats:** Lightweight. Works well with Reveal.js 5.x.

### 8. TOC-Progress

- **What:** A LaTeX Beamer-style progress indicator showing sections as a bar at the bottom with the current section highlighted.
- **Why for this presentation:** Alternative to simplemenu -- lighter weight but less customizable. Good if a full menu bar feels too heavy.
- **Repo:** https://github.com/e-gor/Reveal.js-TOC-Progress
- **Demo:** https://e-gor.github.io/Reveal.js-TOC-Progress/demo
- **Caveats:** Less actively maintained than simplemenu. Check Reveal.js 5.x compatibility before adopting.

### 9. Chalkboard (rajgoel)

- **What:** Adds a chalkboard overlay and slide annotation capability. Draw on slides or switch to a full chalkboard.
- **Why for this presentation:** Could be useful for impromptu diagramming during Q&A or for annotating architecture diagrams while explaining.
- **Repo:** https://github.com/rajgoel/reveal.js-plugins/tree/master/chalkboard
- **Caveats:** Heavier plugin. Requires practice to use well during a live talk. Touch/pen input works best.

---

## For Animated Diagrams

This is the area the user specifically wants to improve. Here are the best options, ranked:

### A. Mermaid (via reveal.js-mermaid-plugin) -- BEST FIT

- **What:** Text-based diagrams rendered in the browser. Supports flowcharts, sequence diagrams, class diagrams, state diagrams, ER diagrams, C4 architecture diagrams (via c4 plugin), and more.
- **Why best for this presentation:**
  - The multi-tenancy spectrum, tenant resolution flow, and database architecture diagrams all map naturally to Mermaid's flowchart/graph syntax.
  - C4 model support (via `%%{init: {'theme': 'base'}}%%`) lets you create proper architecture diagrams.
  - Diagrams can be combined with Reveal.js fragments to animate step-by-step builds.
  - Text-based source means diagrams live in HTML/Markdown, are version-controllable, and easy to edit.
  - Mermaid theming allows matching Info Support brand colors.
- **Animation approach:** Combine Mermaid with Reveal.js fragments by rendering the diagram in stages, or use CSS transitions on the generated SVG elements.

### B. Animate plugin (rajgoel) -- BEST FOR CUSTOM SVG ANIMATION

- **What:** Uses SVG.js to animate SVG elements on slides. Animations are triggered by slide transitions and fragment steps.
- **Why for this presentation:** If you want to keep the existing inline SVG diagrams but add step-by-step animations (e.g., highlight each tenancy model one at a time, animate data flow between components), this is the most direct solution.
- **Repo:** https://github.com/rajgoel/reveal.js-plugins/tree/master/animate
- **Demos:** https://rajgoel.github.io/reveal.js-demos
- **Usage:**
  ```html
  <section data-animate="myanimation.json">
    <svg>... your diagram ...</svg>
  </section>
  ```
  Animation steps are defined in a JSON file referencing SVG element IDs.
- **Caveats:** Requires SVG elements to have IDs. More setup work than Mermaid but gives full control over animation.

### C. D3.js plugins (d3js-plugin, reveald3, diagram-plugin)

- **What:** Embed D3.js-powered interactive visualizations with transitions triggered by slide navigation.
- **Why for this presentation:** Overkill for static architecture diagrams, but powerful if you want truly interactive or data-driven visualizations.
- **Repos:**
  - https://github.com/jlegewie/reveal.js-d3js-plugin (general D3 embedding)
  - https://github.com/gcalmettes/reveal.js-d3 (fragment-aware D3 embedding)
  - https://github.com/teone/reveal.js-diagram-plugin (diagram-focused D3)
- **Caveats:** Requires D3.js knowledge. High effort. Only worth it if you want interactive diagrams that respond to user input, not just animated builds. Some of these have not been updated for Reveal.js 5.x.

### D. PlantUML plugins

- **What:** Render PlantUML diagram markup directly in slides.
- **Why for this presentation:** PlantUML excels at sequence diagrams, class diagrams, and deployment diagrams -- all potentially useful for showing multi-tenant architecture. However, PlantUML requires a server-side renderer (unlike Mermaid which is pure client-side).
- **Repos:**
  - https://github.com/jschildgen/reveal.js-plantuml-plugin
  - https://reveal-plantuml.github.io/
- **Caveats:** Requires a PlantUML server (public or self-hosted). Not ideal for offline conference presentations. Mermaid is the better choice for this use case unless you specifically need PlantUML diagram types.

### E. revealjs-animated

- **What:** Custom CSS/Web Animation API animations for any HTML element on slides.
- **Why for this presentation:** Good for animating non-SVG elements -- for instance, animating boxes or text elements in architecture diagrams built with HTML/CSS instead of SVG.
- **Repo:** https://github.com/rogeralmeida/revealjs-animated
- **Demo:** https://rogeralmeida.github.io/revealjs-animated-examples/
- **Caveats:** Animations are defined via data attributes. Works with Reveal.js 5.x. Less powerful than the SVG-focused Animate plugin for diagram work.

### Recommendation for animated diagrams

**Short-term (lowest effort):** Switch inline SVG diagrams to **Mermaid** via `reveal.js-mermaid-plugin`. This immediately makes diagrams maintainable and adds basic animation through fragment integration.

**Medium-term (richer animation):** Use **Mermaid for structure** + **reveal.js-appearance for entrance animations** on diagram elements. This gives PowerPoint-like build animations without custom SVG work.

**Long-term (full control):** Use the **Animate plugin (rajgoel)** with SVG.js for fully custom, step-by-step diagram animations. Most work but most impressive results.

---

## Not Needed

| Plugin | Why not needed |
|--------|---------------|
| **Math/KaTeX** | No mathematical formulas in this presentation |
| **Audio slideshow** | Not a self-running presentation; live speaker delivery |
| **Seminar/Poll/Questions** | Adds complexity for audience interaction that can be handled by other tools (Slido, etc.) |
| **Leap Motion / Wave / Gamepad / JoyCon** | Novelty input devices not appropriate for conference talk |
| **Speech** | Voice navigation is unreliable in conference environments |
| **Mapbox-GL** | No geographic/map content |
| **Internation (i18n)** | Presentation is single-language |
| **Quizzes** | Not the right format for a technical conference talk |
| **Doghouse (Pug)** | Not a presentation about Pug templating |
| **tldreveal** | Annotation capability better served by the simpler Chalkboard plugin |
| **RevealEditor / reveal-livecode** | Live coding is done via actual IDE demos, not in-slide editors |
| **Tagcloud** | No tag cloud content |
| **MQTT plugin** | No real-time data feeds |
| **ga (Google Analytics)** | Not needed for a conference talk |

---

## Quick-Start: Recommended Plugin Stack

For immediate integration, add these four plugins to the existing setup:

```html
<!-- In <head> -->
<link rel="stylesheet" href="https://cdn.jsdelivr.net/npm/reveal.js-copycode/plugin/copycode/copycode.css" />
<link rel="stylesheet" href="https://cdn.jsdelivr.net/npm/reveal.js-simplemenu/plugin/simplemenu/simplemenu.css" />

<!-- Before </body>, after existing plugin scripts -->
<script src="https://cdn.jsdelivr.net/npm/reveal.js-mermaid-plugin/plugin/mermaid/mermaid.js"></script>
<script src="https://cdn.jsdelivr.net/npm/reveal.js-copycode/plugin/copycode/copycode.js"></script>
<script src="https://cdn.jsdelivr.net/npm/reveal.js-simplemenu/plugin/simplemenu/simplemenu.js"></script>
<script src="https://cdn.jsdelivr.net/npm/reveal.js-plugin-spotlight/spotlight.js"></script>

<script>
  Reveal.initialize({
    hash: true,
    slideNumber: true,
    transition: 'slide',
    center: true,
    progress: true,
    controls: true,
    navigationMode: 'grid',
    width: 960,
    height: 700,
    plugins: [
      RevealNotes,
      RevealHighlight,
      RevealMermaid,
      CopyCode,
      Simplemenu,
      RevealSpotlight
    ],
    // Plugin configs
    copycode: {
      copy: "Copy",
      copied: "Copied!",
    },
    spotlight: {
      size: 60,
      presentingCursor: 'none',
      toggleSpotlightOnMouseDown: false,
      spotlightOnKeyPressAndHold: 81, // Q key
    },
    mermaid: {
      theme: 'base',
      themeVariables: {
        primaryColor: '#003865',
        primaryTextColor: '#fff',
        primaryBorderColor: '#003865',
        lineColor: '#e87722',
        secondaryColor: '#f0f4f8',
      }
    }
  });
</script>
```

**Note:** CDN paths above should be verified before use. Some plugins may need specific version pinning for Reveal.js 5.x compatibility. Always test locally before presenting.
