# Reveal.js Reference Guide

Comprehensive reference for building Reveal.js presentations. Covers setup, markup, features, and customization.

---

## 1. Setup (CDN-based, no npm needed)

Minimal HTML file using CDN links (unpkg or cdnjs):

```html
<!doctype html>
<html>
  <head>
    <meta charset="utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0" />
    <title>My Presentation</title>
    <!-- Core CSS -->
    <link rel="stylesheet" href="https://unpkg.com/reveal.js@5/dist/reveal.css" />
    <!-- Theme (swap black.css for another theme name) -->
    <link rel="stylesheet" href="https://unpkg.com/reveal.js@5/dist/theme/black.css" />
    <!-- Code highlighting theme -->
    <link rel="stylesheet" href="https://unpkg.com/reveal.js@5/plugin/highlight/monokai.css" />
  </head>
  <body>
    <div class="reveal">
      <div class="slides">
        <section>Slide 1</section>
        <section>Slide 2</section>
      </div>
    </div>

    <script src="https://unpkg.com/reveal.js@5/dist/reveal.js"></script>
    <script src="https://unpkg.com/reveal.js@5/plugin/notes/notes.js"></script>
    <script src="https://unpkg.com/reveal.js@5/plugin/highlight/highlight.js"></script>
    <script>
      Reveal.initialize({
        hash: true,
        plugins: [RevealNotes, RevealHighlight],
      });
    </script>
  </body>
</html>
```

**Alternative CDN (cdnjs):**
```
https://cdnjs.cloudflare.com/ajax/libs/reveal.js/5.1.0/reveal.min.js
https://cdnjs.cloudflare.com/ajax/libs/reveal.js/5.1.0/reveal.min.css
https://cdnjs.cloudflare.com/ajax/libs/reveal.js/5.1.0/theme/black.min.css
https://cdnjs.cloudflare.com/ajax/libs/reveal.js/5.1.0/plugin/highlight/highlight.min.js
https://cdnjs.cloudflare.com/ajax/libs/reveal.js/5.1.0/plugin/highlight/monokai.min.css
https://cdnjs.cloudflare.com/ajax/libs/reveal.js/5.1.0/plugin/notes/notes.min.js
```

---

## 2. HTML Structure

The required hierarchy is: `.reveal > .slides > section`

```html
<div class="reveal">
  <div class="slides">
    <section>First slide</section>
    <section>Second slide</section>
    <section>Third slide</section>
  </div>
</div>
```

Each `<section>` is one slide. Slides can contain any HTML: headings, paragraphs, lists, images, code blocks, etc.

---

## 3. Slide Types: Horizontal vs Vertical

**Horizontal slides** (default): Each top-level `<section>` is a horizontal slide, navigated with left/right arrows.

**Vertical slides**: Nest `<section>` elements inside a parent `<section>` to create a vertical stack. Navigate with up/down arrows.

```html
<div class="slides">
  <!-- Horizontal slide 1 -->
  <section>Horizontal Slide</section>

  <!-- Vertical stack -->
  <section>
    <section>Vertical Slide 1 (root)</section>
    <section>Vertical Slide 2</section>
    <section>Vertical Slide 3</section>
  </section>

  <!-- Horizontal slide 3 -->
  <section>Another Horizontal Slide</section>
</div>
```

**Navigation modes** (set in config):
- `"default"` - left/right for horizontal, up/down for vertical, space steps through all
- `"linear"` - left/right step through all slides (horizontal and vertical)
- `"grid"` - stepping left/right between vertical stacks preserves vertical index

---

## 4. Text & Content

Use standard HTML inside `<section>` elements:

```html
<section>
  <h1>Title Slide</h1>
  <p>Subtitle or description</p>
</section>

<section>
  <h2>Slide with Lists</h2>
  <ul>
    <li>First point</li>
    <li>Second point</li>
    <li>Third point</li>
  </ul>
</section>

<section>
  <h2>Two Columns</h2>
  <div style="display: flex; gap: 2em;">
    <div style="flex: 1;">
      <h3>Left Column</h3>
      <p>Content here</p>
    </div>
    <div style="flex: 1;">
      <h3>Right Column</h3>
      <p>Content here</p>
    </div>
  </div>
</section>
```

---

## 5. Code Blocks

Powered by highlight.js via the `RevealHighlight` plugin.

### Basic Code Block
```html
<pre><code data-trim data-noescape>
function hello() {
  console.log("Hello, world!");
}
</code></pre>
```

- `data-trim` - removes surrounding whitespace
- `data-noescape` - prevents HTML escaping (use when code contains HTML entities you want preserved)

### Specify Language
```html
<pre><code data-trim class="language-csharp">
public class Startup
{
    public void Configure(IApplicationBuilder app) { }
}
</code></pre>
```

### Line Numbers
Add `data-line-numbers` to enable line numbers:
```html
<pre><code data-trim data-line-numbers>
line 1
line 2
line 3
</code></pre>
```

### Highlight Specific Lines
```html
<!-- Highlight lines 3 and 8-10 -->
<pre><code data-trim data-line-numbers="3,8-10">
...code...
</code></pre>
```

### Step-by-step Line Highlighting
Use `|` to separate steps. Each step highlights different lines as you advance:
```html
<!-- Step 1: lines 1-3, Step 2: lines 5-7, Step 3: line 9 -->
<pre><code data-trim data-line-numbers="1-3|5-7|9">
...code...
</code></pre>
```

### Line Number Offset
Start line numbers from a specific number:
```html
<pre><code data-trim data-line-numbers data-ln-start-from="7">
...code starting from line 7...
</code></pre>
```

### HTML Entities in Code
Wrap code in `<script type="text/template">` to avoid manual escaping:
```html
<pre><code><script type="text/template">
sealed class Either<out A, out B> {
  data class Left<out A>(val a: A) : Either<A, Nothing>()
}
</script></code></pre>
```

### Code Highlight Theme
Include a theme CSS file. Default is Monokai:
```html
<link rel="stylesheet" href="https://unpkg.com/reveal.js@5/plugin/highlight/monokai.css" />
```
Full list at https://highlightjs.org/demo/

---

## 6. Fragments (Incremental Reveal)

Add `class="fragment"` to any element to reveal it step-by-step:

```html
<section>
  <p class="fragment">Appears first</p>
  <p class="fragment">Appears second</p>
  <p class="fragment">Appears third</p>
</section>
```

### Fragment Animation Styles

| Class | Effect |
|-------|--------|
| `fade-in` | Default: fade in |
| `fade-out` | Start visible, fade out |
| `fade-up` | Slide up while fading in |
| `fade-down` | Slide down while fading in |
| `fade-left` | Slide left while fading in |
| `fade-right` | Slide right while fading in |
| `fade-in-then-out` | Fade in, then out on next step |
| `current-visible` | Fade in, then out on next step |
| `fade-in-then-semi-out` | Fade in, then to 50% on next step |
| `grow` | Scale up |
| `semi-fade-out` | Fade out to 50% |
| `shrink` | Scale down |
| `strike` | Strikethrough |
| `highlight-red` | Turn text red |
| `highlight-green` | Turn text green |
| `highlight-blue` | Turn text blue |
| `highlight-current-red` | Turn text red, then back on next step |
| `highlight-current-green` | Turn text green, then back on next step |
| `highlight-current-blue` | Turn text blue, then back on next step |

```html
<p class="fragment fade-up">Slides up while fading in</p>
<p class="fragment highlight-red">Turns red</p>
<p class="fragment fade-in-then-out">Appears then disappears</p>
```

### Fragment Order
Control order with `data-fragment-index`:
```html
<p class="fragment" data-fragment-index="3">Appears last</p>
<p class="fragment" data-fragment-index="1">Appears first</p>
<p class="fragment" data-fragment-index="2">Appears second</p>
```

### Nested Fragments
Apply multiple effects sequentially:
```html
<span class="fragment fade-in">
  <span class="fragment highlight-red">
    <span class="fragment fade-out">
      Fade in, turn red, fade out
    </span>
  </span>
</span>
```

### Custom Fragment Styles
```html
<style>
  .fragment.blur {
    filter: blur(5px);
  }
  .fragment.blur.visible {
    filter: none;
  }
</style>
<p class="fragment custom blur">Starts blurred, becomes clear</p>
```

---

## 7. Backgrounds

Add `data-background-*` attributes to `<section>` elements for full-page backgrounds.

### Color Background
```html
<section data-background-color="aquamarine">
  <h2>Color Background</h2>
</section>

<section data-background-color="#4287f5">
  <h2>Hex Color</h2>
</section>
```

### Gradient Background
```html
<section data-background-gradient="linear-gradient(to bottom, #283b95, #17b2c3)">
  <h2>Gradient</h2>
</section>

<section data-background-gradient="radial-gradient(#283b95, #17b2c3)">
  <h2>Radial Gradient</h2>
</section>
```

### Image Background
```html
<section data-background-image="path/to/image.jpg"
         data-background-size="cover"
         data-background-position="center"
         data-background-repeat="no-repeat"
         data-background-opacity="0.5">
  <h2>Image Background</h2>
</section>
```

| Attribute | Default | Description |
|-----------|---------|-------------|
| `data-background-image` | | URL of the image |
| `data-background-size` | `cover` | CSS background-size |
| `data-background-position` | `center` | CSS background-position |
| `data-background-repeat` | `no-repeat` | CSS background-repeat |
| `data-background-opacity` | `1` | Opacity 0-1 |

### Video Background
```html
<section data-background-video="video.mp4"
         data-background-video-loop
         data-background-video-muted
         data-background-size="cover"
         data-background-opacity="0.5">
  <h2>Video Background</h2>
</section>
```

### Iframe Background
```html
<section data-background-iframe="https://example.com"
         data-background-interactive>
  <h2>Interactive iframe background</h2>
</section>
```

### Background Transitions
```javascript
Reveal.initialize({
  backgroundTransition: 'slide', // none/fade/slide/convex/concave/zoom
});
```
Or per-slide: `<section data-background-transition="zoom">`

### Parallax Background
```javascript
Reveal.initialize({
  parallaxBackgroundImage: 'https://example.com/bg.jpg',
  parallaxBackgroundSize: '2100px 900px',
  parallaxBackgroundHorizontal: 200,
  parallaxBackgroundVertical: 50,
});
```

---

## 8. Speaker Notes

Add `<aside class="notes">` inside a slide. Press **S** to open speaker view.

```html
<section>
  <h2>My Slide</h2>
  <p>Visible content</p>

  <aside class="notes">
    These are private speaker notes. Only visible in the speaker view.
    - Talk about X
    - Mention Y
  </aside>
</section>
```

Alternative: use `data-notes` attribute:
```html
<section data-notes="Speaker notes go here">
  <h2>Slide content</h2>
</section>
```

**Plugin setup** (required):
```html
<script src="https://unpkg.com/reveal.js@5/plugin/notes/notes.js"></script>
<script>
  Reveal.initialize({
    plugins: [RevealNotes],
  });
</script>
```

**Speaker view features:**
- Preview of current and next slide
- Speaker timer (click to reset)
- Pacing timer (configure with `defaultTiming` or `totalTime`)
- Per-slide timing with `data-timing="120"` attribute (seconds)

**Show notes to all viewers:**
```javascript
Reveal.initialize({ showNotes: true });
// Or on separate page for PDF:
Reveal.initialize({ showNotes: 'separate-page' });
```

---

## 9. Transitions

### Slide Transitions

Available styles: `none`, `fade`, `slide` (default), `convex`, `concave`, `zoom`

**Global (in config):**
```javascript
Reveal.initialize({
  transition: 'slide',        // none/fade/slide/convex/concave/zoom
  transitionSpeed: 'default',  // default/fast/slow
});
```

**Per-slide:**
```html
<section data-transition="zoom">
  <h2>This slide zooms in!</h2>
</section>

<section data-transition-speed="fast">
  <h2>Fast transition</h2>
</section>
```

### Separate In/Out Transitions
```html
<section data-transition="slide-in fade-out">
  <h2>Slides in, fades out</h2>
</section>

<section data-transition="fade-in slide-out">
  <h2>Fades in, slides out</h2>
</section>
```

### Background Transitions
```javascript
Reveal.initialize({
  backgroundTransition: 'fade', // none/fade/slide/convex/concave/zoom
});
```
Per-slide: `<section data-background-transition="zoom">`

---

## 10. Configuration Options

Key `Reveal.initialize()` options:

```javascript
Reveal.initialize({
  // --- Navigation & Controls ---
  controls: true,              // Show navigation arrows
  controlsTutorial: true,      // Show control hints
  controlsLayout: 'bottom-right', // 'edges' or 'bottom-right'
  progress: true,              // Show progress bar
  slideNumber: false,          // true, 'h.v', 'h/v', 'c', 'c/t'
  hash: true,                  // Add slide number to URL hash
  history: false,              // Push each slide change to browser history
  keyboard: true,              // Enable keyboard navigation
  overview: true,              // Enable overview mode (Esc key)
  touch: true,                 // Touch navigation
  loop: false,                 // Loop presentation
  navigationMode: 'default',   // 'default', 'linear', 'grid'
  shuffle: false,              // Randomize slide order

  // --- Appearance ---
  center: true,                // Vertical centering
  transition: 'slide',         // none/fade/slide/convex/concave/zoom
  transitionSpeed: 'default',  // default/fast/slow
  backgroundTransition: 'fade',

  // --- Fragments ---
  fragments: true,             // Enable fragments globally
  fragmentInURL: true,         // Include fragment in URL hash

  // --- Auto Features ---
  autoSlide: 0,                // Auto-advance (ms), 0 = disabled
  autoSlideStoppable: true,
  autoPlayMedia: null,         // null/true/false
  autoAnimate: true,
  autoAnimateEasing: 'ease',
  autoAnimateDuration: 1.0,
  autoAnimateUnmatched: true,

  // --- Media & Loading ---
  preloadIframes: null,
  viewDistance: 3,             // Slides to preload
  mobileViewDistance: 2,

  // --- Speaker Notes ---
  showNotes: false,            // true / 'separate-page'
  defaultTiming: null,         // Seconds per slide for pacing timer
  totalTime: null,             // Total presentation time in seconds

  // --- PDF ---
  pdfMaxPagesPerSlide: Infinity,
  pdfSeparateFragments: true,

  // --- Display ---
  width: 960,                  // Presentation width
  height: 700,                 // Presentation height
  margin: 0.04,                // Margin around slides
  minScale: 0.2,
  maxScale: 2.0,
  disableLayout: false,        // Disable scaling/centering for custom CSS

  // --- Plugins ---
  plugins: [RevealNotes, RevealHighlight],
});
```

---

## 11. Themes

### Built-in Themes

| Theme | File |
|-------|------|
| black (default) | `theme/black.css` |
| white | `theme/white.css` |
| league | `theme/league.css` |
| beige | `theme/beige.css` |
| night | `theme/night.css` |
| serif | `theme/serif.css` |
| simple | `theme/simple.css` |
| solarized | `theme/solarized.css` |
| moon | `theme/moon.css` |
| dracula | `theme/dracula.css` |
| sky | `theme/sky.css` |
| blood | `theme/blood.css` |

Switch theme by changing the CSS link:
```html
<link rel="stylesheet" href="https://unpkg.com/reveal.js@5/dist/theme/white.css" />
```

### Custom Theme (CSS Override)

All theme variables are CSS custom properties on `:root`. You can override them or create a fully custom theme:

```html
<style>
  :root {
    --r-background-color: #191919;
    --r-main-font: 'Arial', sans-serif;
    --r-main-font-size: 42px;
    --r-main-color: #fff;
    --r-heading-font: 'Arial', sans-serif;
    --r-heading-color: #58a6ff;
    --r-heading-font-weight: 700;
    --r-heading-text-transform: none;
    --r-heading1-size: 2.5em;
    --r-heading2-size: 1.6em;
    --r-heading3-size: 1.3em;
    --r-heading4-size: 1.0em;
    --r-link-color: #58a6ff;
    --r-link-color-hover: #79b8ff;
    --r-link-color-dark: #1f6feb;
    --r-selection-background-color: rgba(88, 166, 255, 0.75);
    --r-selection-color: #fff;
    --r-code-font: 'Fira Code', monospace;
    --r-block-margin: 20px;
  }
</style>
```

**Key CSS custom properties:**
- `--r-background-color` - slide background
- `--r-main-font` - body text font
- `--r-main-font-size` - body text size (default 42px)
- `--r-main-color` - body text color
- `--r-heading-font` - heading font
- `--r-heading-color` - heading color
- `--r-heading-font-weight` - heading weight
- `--r-heading-text-transform` - heading transform (e.g., `none`, `uppercase`)
- `--r-heading1-size` through `--r-heading4-size` - heading sizes
- `--r-link-color` / `--r-link-color-hover` / `--r-link-color-dark` - link colors
- `--r-selection-background-color` / `--r-selection-color` - text selection
- `--r-code-font` - code font family
- `--r-block-margin` - margin between block elements

You can also add a completely blank theme stylesheet and style everything from scratch:
```html
<!-- Use reveal.css only, no theme, then add all custom styles -->
<link rel="stylesheet" href="https://unpkg.com/reveal.js@5/dist/reveal.css" />
<style>
  /* Your complete custom theme here */
</style>
```

---

## 12. Media (Images, Video, Iframes)

### Images
```html
<section>
  <h2>Image Slide</h2>
  <img src="image.png" alt="Description" width="600" />
</section>
```

### Lazy Loading
Change `src` to `data-src` for lazy loading (loads only when near current slide):
```html
<img data-src="image.png" />
<video>
  <source data-src="video.mp4" type="video/mp4" />
</video>
<iframe data-src="https://example.com"></iframe>
```

### Auto-playing Media
```html
<video data-autoplay src="video.mp4"></video>
<audio data-autoplay src="audio.mp3"></audio>
```

Global setting:
```javascript
Reveal.initialize({ autoPlayMedia: true }); // true/false/null
```

### Iframes
```html
<section>
  <iframe src="https://example.com" width="800" height="500" frameborder="0"></iframe>
</section>
```

Iframes receive `slide:start` and `slide:stop` postMessage events.

For full-page iframes, use iframe backgrounds instead (see Backgrounds section).

---

## 13. Auto-Animate

Automatically animate elements between slides by adding `data-auto-animate` to consecutive `<section>` elements.

### Basic Example
```html
<section data-auto-animate>
  <h1>Auto-Animate</h1>
</section>
<section data-auto-animate>
  <h1 style="margin-top: 100px; color: red;">Auto-Animate</h1>
</section>
```

### Element Matching
Elements are matched by:
1. `data-id` attribute (highest priority) - use for explicit matching
2. Text content + node type (for text elements)
3. `src` attribute (for images, videos, iframes)
4. DOM order

```html
<section data-auto-animate>
  <div data-id="box" style="height: 50px; background: salmon;"></div>
</section>
<section data-auto-animate>
  <div data-id="box" style="height: 200px; background: blue;"></div>
</section>
```

### Animating Code Blocks
Requires `data-line-numbers` and matching `data-id`:
```html
<section data-auto-animate>
  <pre data-id="code"><code data-trim data-line-numbers>
let planets = [
  { name: 'mars', diameter: 6779 },
]
  </code></pre>
</section>
<section data-auto-animate>
  <pre data-id="code"><code data-trim data-line-numbers>
let planets = [
  { name: 'mars', diameter: 6779 },
  { name: 'earth', diameter: 12742 },
  { name: 'jupiter', diameter: 139820 }
]
  </code></pre>
</section>
```

### Animating Lists
List items are matched individually:
```html
<section data-auto-animate>
  <ul>
    <li>Mercury</li>
    <li>Jupiter</li>
  </ul>
</section>
<section data-auto-animate>
  <ul>
    <li>Mercury</li>
    <li>Earth</li>
    <li>Jupiter</li>
    <li>Saturn</li>
  </ul>
</section>
```

### Animation Settings

| Attribute | Default | Description |
|-----------|---------|-------------|
| `data-auto-animate-easing` | `ease` | CSS easing function |
| `data-auto-animate-duration` | `1.0` | Duration in seconds |
| `data-auto-animate-unmatched` | `true` | Fade in unmatched elements |
| `data-auto-animate-delay` | `0` | Delay in seconds (per-element only) |
| `data-auto-animate-id` | | Group ID for separate auto-animate groups |
| `data-auto-animate-restart` | | Break auto-animate between adjacent slides |

Global defaults:
```javascript
Reveal.initialize({
  autoAnimateEasing: 'ease-out',
  autoAnimateDuration: 0.8,
  autoAnimateUnmatched: false,
});
```

### Separate Auto-Animate Groups
```html
<section data-auto-animate>
  <h1>Group A</h1>
</section>
<section data-auto-animate>
  <h1 style="color: #3B82F6;">Group A</h1>
</section>
<section data-auto-animate data-auto-animate-id="two">
  <h1>Group B</h1>
</section>
<section data-auto-animate data-auto-animate-id="two">
  <h1 style="color: #10B981;">Group B</h1>
</section>
```

### Animatable Properties
CSS properties that auto-animate supports: `opacity`, `color`, `background-color`, `padding`, `font-size`, `line-height`, `letter-spacing`, `border-width`, `border-color`, `border-radius`, `outline`, `outline-offset`. Position and scale are handled separately via transforms.

---

## 14. Layout Helpers

### r-fit-text
Makes text as large as possible without overflowing:
```html
<section>
  <h2 class="r-fit-text">BIG TEXT</h2>
</section>

<section>
  <h2 class="r-fit-text">FIRST LINE</h2>
  <h2 class="r-fit-text">SECOND LINE</h2>
</section>
```

### r-stretch
Resize an element to fill remaining vertical space:
```html
<section>
  <h2>Title</h2>
  <img class="r-stretch" src="image.png" />
  <p>Caption</p>
</section>
```
Limitations:
- Only direct descendants of `<section>` can be stretched
- Only one element per slide can use `r-stretch`

### r-stack
Center and stack multiple elements on top of each other (great with fragments):
```html
<div class="r-stack">
  <img class="fragment" src="img1.png" width="450" height="300" />
  <img class="fragment" src="img2.png" width="300" height="450" />
  <img class="fragment" src="img3.png" width="400" height="400" />
</div>
```

Show one at a time:
```html
<div class="r-stack">
  <img class="fragment fade-out" data-fragment-index="0" src="img1.png" />
  <img class="fragment current-visible" data-fragment-index="0" src="img2.png" />
  <img class="fragment" src="img3.png" />
</div>
```

### r-hstack and r-vstack
Horizontal and vertical flex stacks:
```html
<div class="r-hstack">
  <div>Left</div>
  <div>Center</div>
  <div>Right</div>
</div>

<div class="r-vstack">
  <div>Top</div>
  <div>Middle</div>
  <div>Bottom</div>
</div>
```

### r-frame
Decorative frame for elements:
```html
<a href="#">
  <img class="r-frame" src="logo.svg" width="200" />
</a>
```

---

## 15. PDF Export

### Browser Print Method
1. Add `?print-pdf` to the URL: `http://localhost:8000/?print-pdf`
2. Open print dialog (Ctrl/Cmd+P)
3. Set Destination: Save as PDF
4. Set Layout: Landscape
5. Set Margins: None
6. Enable: Background graphics
7. Click Save

**Note:** Only confirmed to work in Chrome/Chromium.

### Configuration
```javascript
Reveal.initialize({
  // Max pages per slide (default: unlimited)
  pdfMaxPagesPerSlide: 1,

  // Print each fragment on separate slide (default: true)
  pdfSeparateFragments: false,

  // Include speaker notes in PDF
  showNotes: true,
  // Or on separate page:
  showNotes: 'separate-page',
});
```

### Alternative: decktape
Command-line PDF export tool: https://github.com/astefanutti/decktape

---

## 16. Keyboard Shortcuts

| Key | Action |
|-----|--------|
| N, Space, Right | Next slide |
| P, Left | Previous slide |
| Up | Navigate up (vertical) |
| Down | Navigate down (vertical) |
| Home | First slide |
| End | Last slide |
| F | Fullscreen |
| S | Speaker notes view |
| O / Esc | Overview mode |
| B / . | Pause (blackout) |
| ? | Show keyboard shortcuts |

---

## 17. Slide State & Data Attributes Quick Reference

| Attribute | On | Purpose |
|-----------|-----|---------|
| `data-background-color` | `<section>` | Solid background color |
| `data-background-gradient` | `<section>` | Gradient background |
| `data-background-image` | `<section>` | Image background |
| `data-background-video` | `<section>` | Video background |
| `data-background-iframe` | `<section>` | Iframe background |
| `data-background-interactive` | `<section>` | Make iframe bg interactive |
| `data-background-opacity` | `<section>` | Background opacity (0-1) |
| `data-background-size` | `<section>` | Background size |
| `data-background-position` | `<section>` | Background position |
| `data-background-transition` | `<section>` | Per-slide bg transition |
| `data-transition` | `<section>` | Per-slide transition |
| `data-transition-speed` | `<section>` | Per-slide transition speed |
| `data-auto-animate` | `<section>` | Enable auto-animate |
| `data-auto-animate-id` | `<section>` | Group auto-animate slides |
| `data-auto-animate-restart` | `<section>` | Break auto-animate chain |
| `data-auto-animate-easing` | `<section>` or element | Easing function |
| `data-auto-animate-duration` | `<section>` or element | Duration (seconds) |
| `data-id` | any element | Match elements for auto-animate |
| `data-fragment-index` | `.fragment` | Control fragment order |
| `data-state` | `<section>` | Add class to viewport |
| `data-notes` | `<section>` | Speaker notes |
| `data-timing` | `<section>` | Pacing time (seconds) |
| `data-autoplay` | `<video>`/`<audio>` | Auto-play media |
| `data-src` | media/iframe | Lazy-load source |
| `data-preload` | `<section>`/`<iframe>` | Preload content |
| `data-trim` | `<code>` | Trim whitespace |
| `data-noescape` | `<code>` | Don't escape HTML |
| `data-line-numbers` | `<code>` | Show line numbers / highlight |
| `data-ln-start-from` | `<code>` | Start line number offset |
| `class="fragment"` | any element | Incremental reveal |
| `class="r-fit-text"` | text element | Auto-size text |
| `class="r-stretch"` | element | Fill remaining space |
| `class="r-stack"` | container | Stack children |
| `class="r-hstack"` | container | Horizontal stack |
| `class="r-vstack"` | container | Vertical stack |
| `class="r-frame"` | element | Decorative frame |

---

## 18. Complete Minimal Example

```html
<!doctype html>
<html>
  <head>
    <meta charset="utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0" />
    <title>Presentation</title>
    <link rel="stylesheet" href="https://unpkg.com/reveal.js@5/dist/reveal.css" />
    <link rel="stylesheet" href="https://unpkg.com/reveal.js@5/dist/theme/white.css" />
    <link rel="stylesheet" href="https://unpkg.com/reveal.js@5/plugin/highlight/monokai.css" />
    <style>
      /* Custom overrides */
      :root {
        --r-heading-color: #2d3748;
      }
    </style>
  </head>
  <body>
    <div class="reveal">
      <div class="slides">

        <!-- Title slide -->
        <section>
          <h1 class="r-fit-text">My Presentation</h1>
          <p>Author Name</p>
          <aside class="notes">Welcome everyone!</aside>
        </section>

        <!-- Content with fragments -->
        <section>
          <h2>Key Points</h2>
          <ul>
            <li class="fragment">First point</li>
            <li class="fragment">Second point</li>
            <li class="fragment">Third point</li>
          </ul>
        </section>

        <!-- Code slide -->
        <section>
          <h2>Code Example</h2>
          <pre><code data-trim data-line-numbers="1|3-4|6">
function greet(name) {
  // Build greeting
  const greeting = `Hello, ${name}!`;
  console.log(greeting);
  // Return it
  return greeting;
}
          </code></pre>
        </section>

        <!-- Auto-animate slides -->
        <section data-auto-animate>
          <h2>Growing Code</h2>
          <pre data-id="code"><code data-trim data-line-numbers>
const app = express();
          </code></pre>
        </section>
        <section data-auto-animate>
          <h2>Growing Code</h2>
          <pre data-id="code"><code data-trim data-line-numbers>
const app = express();
app.get('/', (req, res) => {
  res.send('Hello World');
});
app.listen(3000);
          </code></pre>
        </section>

        <!-- Background slide -->
        <section data-background-color="#1a1a2e">
          <h2 style="color: white;">Dark Background</h2>
        </section>

        <!-- Two-column layout -->
        <section>
          <h2>Comparison</h2>
          <div class="r-hstack" style="gap: 2em; align-items: start;">
            <div>
              <h3>Before</h3>
              <p>Old approach</p>
            </div>
            <div>
              <h3>After</h3>
              <p>New approach</p>
            </div>
          </div>
        </section>

        <!-- End slide -->
        <section>
          <h1>Thank You</h1>
          <p>Questions?</p>
        </section>

      </div>
    </div>

    <script src="https://unpkg.com/reveal.js@5/dist/reveal.js"></script>
    <script src="https://unpkg.com/reveal.js@5/plugin/notes/notes.js"></script>
    <script src="https://unpkg.com/reveal.js@5/plugin/highlight/highlight.js"></script>
    <script>
      Reveal.initialize({
        hash: true,
        transition: 'slide',
        plugins: [RevealNotes, RevealHighlight],
      });
    </script>
  </body>
</html>
```
