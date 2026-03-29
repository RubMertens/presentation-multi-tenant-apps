# C#/.NET Syntax Highlighting Options for Reveal.js

## Current Setup

The presentation uses Reveal.js 5.x with the built-in **highlight plugin** (powered by highlight.js) and the **Monokai** theme. Code blocks use a dark blue background (`#1e2a38`) with custom CSS overrides in `infosupport-theme.css` that map hljs classes to Info Support brand colors.

### C# Features Used in the Presentation

The code slides use these C# features that need proper highlighting:
- Primary constructors (`class Foo(Bar bar) : IFoo`)
- Generics (`List<T>`, `IOptionsSnapshot<AvailableTenants>`)
- Lambda expressions (`p => p.GetRequiredService<T>()`)
- Pattern matching (`entry is { Entity: ITenanted, Context: ApplicationDbContext db }`)
- String interpolation (`$"..."`)
- LINQ methods (`.Where()`, `.FirstOrDefault()`)
- async/await, Task<T>
- Expression trees (`Expression<Func<T, bool>>`)
- Init-only setters, records
- Extension methods
- Attributes (`[Attribute]`)

### Current Limitations

highlight.js's C# grammar recognizes keywords, strings, comments, numbers, and some types, but:
- **Does NOT highlight type names** (e.g., `TenantContext`, `ApplicationDbContext` appear as plain text)
- **Does NOT highlight method names** (e.g., `InvokeAsync`, `CreateAsync` appear as plain text)
- **No special handling for primary constructors** (a C# 12 feature)
- **Generic type parameters** are partially handled but not always styled distinctly
- **Limited scope classes** mean themes can only differentiate ~10 token categories

The `#2500` higher-fidelity initiative at highlight.js has stalled -- maintainers decided against adding new CSS classes to core due to theme maintenance burden.

---

## Option 1: Shiki (TextMate Grammar Engine)

### Overview
Shiki uses the same TextMate grammars and themes as VS Code. C# highlighting would be identical to what developers see in their editor -- the gold standard for accuracy.

### C# Grammar Quality: EXCELLENT
- Uses the official VS Code C# TextMate grammar
- Handles ALL modern C# features: primary constructors, pattern matching, generics, string interpolation, LINQ, async/await, expression trees, nullable types, records
- Highlights type names, method names, properties, and parameters distinctly
- Supports 50+ token scopes for C# (vs ~10 in highlight.js)

### Available Themes (60+ total)
Dark themes particularly suitable for the presentation:
- **`dark-plus`** -- VS Code's default dark theme (excellent C# support)
- **`github-dark`** / **`github-dark-dimmed`** -- GitHub's dark themes
- **`one-dark-pro`** -- Popular Atom-inspired theme
- **`vitesse-dark`** -- Anthony Fu's clean dark theme
- **`tokyo-night`** -- Popular dark blue theme
- **`nord`** -- Arctic blue-toned theme
- **`catppuccin-mocha`** / **`catppuccin-macchiato`** -- Warm dark themes

### Integration with Reveal.js
**No official plugin exists.** The Reveal.js maintainer acknowledged interest in Shiki support (issue #3587) but recommended creating a custom plugin. Integration approach:

```html
<!-- CDN approach (no build step) -->
<script type="module">
  import { codeToHtml } from 'https://esm.sh/shiki@3.0.0'

  // After Reveal loads, replace code blocks
  document.querySelectorAll('pre code.language-csharp').forEach(async (el) => {
    const code = el.textContent
    const html = await codeToHtml(code, {
      lang: 'csharp',
      theme: 'dark-plus'
    })
    el.closest('pre').outerHTML = html
  })
</script>
```

### Caveats
- **WASM dependency**: Shiki loads a 231 KB (gzipped) WASM binary for the TextMate engine
- **Async initialization**: All highlighting is async; code blocks flash unstyled before highlighting
- **~200 KB per language/theme** loaded on demand (C# + 1 theme = ~400 KB additional)
- **data-line-numbers breaks**: Reveal.js's `data-line-numbers` feature is tightly coupled to highlight.js. Shiki produces different HTML structure, so line numbers and step-by-step line highlighting would need a custom implementation
- **data-auto-animate breaks**: Code auto-animate relies on highlight.js token structure
- **No `data-trim`/`data-noescape` processing**: These are handled by the Reveal highlight plugin
- **Inline styles vs classes**: Shiki uses inline `style` attributes by default (not CSS classes), which conflicts with CSS theme overrides
- **Performance**: highlight.js is 44x faster than Shiki in benchmarks. For 60 slides loaded client-side, expect a visible delay

### Effort: HIGH
Would require building a custom Reveal.js plugin that:
1. Processes `data-trim` and `data-noescape` manually
2. Implements line numbering and line highlighting (`data-line-numbers`)
3. Handles step-by-step highlight animations (`|` syntax)
4. Supports `data-auto-animate` code transitions
5. Manages async loading without flash-of-unstyled-code

---

## Option 2: Prism.js

### Overview
Prism.js is a lightweight, modular syntax highlighter. Its C# grammar received a complete rewrite for C# 8.0 features.

### C# Grammar Quality: GOOD
- Complete rewrite for C# 8.0 (2020) added: attributes, generics with variance, string interpolation, pattern matching, range operators, named parameters
- Token granularity is better than highlight.js: has `return-type`, `constructor-invocation`, `named-parameter`, `attribute`, `generic-constraint`, `type-expression` tokens
- However, grammar hasn't been updated since 2020 (no C# 10-12 features like primary constructors, file-scoped namespaces, raw string literals)

### Maintenance Status: LOW
- Prism.js has not released a new npm version in 12+ months
- Still gets 11.9M weekly downloads but is effectively in maintenance mode
- Community has discussed replacing it in projects (e.g., Obsidian forum thread)
- Not actively adding new language features

### Integration with Reveal.js
**No official Reveal.js plugin exists.** Would need to:
1. Remove the highlight plugin from Reveal.initialize()
2. Load Prism core + C# language + line-numbers plugin + line-highlight plugin
3. Re-implement Reveal.js's `data-line-numbers` step animation

### Caveats
- **Same line-numbers problem as Shiki**: Reveal.js's line highlighting is built into its highlight plugin, not Prism
- **Maintenance risk**: Library is effectively stalled
- **No primary constructors**: Grammar is frozen at C# 8.0
- **Different class names**: Would need to rewrite all custom CSS token colors

### Effort: HIGH
Similar integration challenges as Shiki, plus using a stalling library.

---

## Option 3: Improve highlight.js Configuration (RECOMMENDED)

### Overview
The current highlight.js setup is already 80% there. The custom CSS in `infosupport-theme.css` already maps token colors to Info Support brand colors. The main improvements are:

### 3A. Switch to a Better Base Theme

Replace Monokai with **`vs2015`** or **`github-dark`** (both ship with highlight.js):

```html
<!-- Replace this -->
<link rel="stylesheet" href="https://unpkg.com/reveal.js@5/plugin/highlight/monokai.css" />

<!-- With this -->
<link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/highlight.js/11.9.0/styles/vs2015.min.css" />
```

The `vs2015` theme is inspired by Visual Studio's dark theme and has better token differentiation for C#-like languages. However, since the presentation already overrides all token colors via `infosupport-theme.css`, the base theme matters less.

### 3B. Expand Custom CSS Token Colors

The current custom CSS covers: `.hljs-keyword`, `.hljs-string`, `.hljs-number`, `.hljs-comment`, `.hljs-function`, `.hljs-title`, `.hljs-built_in`, `.hljs-params`, `.hljs-meta`, `.hljs-literal`, `.hljs-attr`.

Add coverage for additional scopes that highlight.js emits for C#:

```css
/* Types and class titles */
.reveal pre code .hljs-title.class_ {
  color: #4EC9B0;  /* Teal green - matches VS Code type color */
}

/* Function/method definitions */
.reveal pre code .hljs-title.function_ {
  color: #DCDCAA;  /* Light yellow - matches VS Code method color */
}

/* Variable names and properties */
.reveal pre code .hljs-variable,
.reveal pre code .hljs-property {
  color: #9CDCFE;  /* Light blue */
}

/* Operators */
.reveal pre code .hljs-operator {
  color: #D4D4D4;  /* Light gray */
}

/* Punctuation (braces, parens, semicolons) */
.reveal pre code .hljs-punctuation {
  color: #D4D4D4;
}

/* Preprocessor directives (#region, #if) */
.reveal pre code .hljs-meta .hljs-keyword {
  color: #C586C0;  /* Purple - matches VS Code preprocessor */
}

/* XML doc comments */
.reveal pre code .hljs-doctag {
  color: #608B4E;  /* Green - matches VS Code XML doc */
}

/* Template/interpolation expressions */
.reveal pre code .hljs-subst {
  color: #e4e8ec;  /* Default text color */
}
```

### 3C. Use VS Code Dark+ Inspired Color Palette

Map the Info Support brand colors to a VS Code Dark+ inspired palette that C# developers will find familiar:

| Token | Current Color | Suggested Color | VS Code Reference |
|-------|--------------|-----------------|-------------------|
| Keywords (`class`, `public`, `return`) | `#6ECFF6` (IS light blue) | `#569CD6` or keep `#6ECFF6` | Blue |
| Types (`string`, `int`, `bool`) | `#6ECFF6` (same as keywords) | `#4EC9B0` | Teal/green |
| Strings | `#BED62F` (IS green) | Keep `#BED62F` | Orange in VS Code, but green works well |
| Numbers | `#D0E26A` | Keep `#D0E26A` | Light green |
| Comments | `#6a8299` | Keep `#6a8299` | Green/gray |
| Methods/functions | `#00A3E0` (IS sky blue) | `#DCDCAA` or keep `#00A3E0` | Yellow |
| Built-in types | `#6ECFF6` | `#4EC9B0` | Teal |
| Parameters | `#e4e8ec` | `#9CDCFE` | Light blue |
| Attributes/meta | `#BED62F` | Keep `#BED62F` | Green |

### 3D. Differentiate Keywords from Types

The biggest win: currently keywords AND types share the same color (`#6ECFF6`). highlight.js does emit different classes for these. Split them:

```css
/* Keywords: blue */
.reveal pre code .hljs-keyword {
  color: #6ECFF6;
}

/* Built-in types: teal (visually distinct from keywords) */
.reveal pre code .hljs-type,
.reveal pre code .hljs-built_in {
  color: #4EC9B0;
}
```

This single change significantly improves readability of C# code.

### What This Preserves
- All existing Reveal.js features work: `data-line-numbers`, step animations (`|`), `data-auto-animate`, `data-trim`, `data-noescape`
- No new dependencies or WASM loading
- No async rendering or flash of unstyled code
- CopyCode plugin continues to work
- Zero migration effort for existing slides

### Effort: LOW
~30 minutes to update CSS, no structural changes needed.

---

## Option 4: highlight.js with Updated/Custom C# Grammar

### Overview
Load a newer or custom C# grammar that adds more token scopes.

### Approach
Use the `beforeHighlight` callback in Reveal.js to register an enhanced grammar:

```js
Reveal.initialize({
  highlight: {
    beforeHighlight: (hljs) => {
      // Could register a custom/enhanced C# grammar here
      // hljs.registerLanguage('csharp', enhancedCSharpGrammar);
    }
  }
});
```

### Caveats
- No community-maintained "enhanced C# grammar" exists for highlight.js
- Writing one from scratch is substantial work (the existing grammar is ~300 lines)
- Risk of regressions with edge cases
- Would need ongoing maintenance as C# evolves

### Effort: VERY HIGH
Not recommended unless the team is willing to maintain a custom grammar.

---

## Recommendation

### Go with Option 3: Improve highlight.js Configuration

**Rationale:**
1. **Lowest risk**: No new dependencies, no breaking changes to existing Reveal.js features
2. **Lowest effort**: CSS-only changes, implementable in under an hour
3. **Good enough quality**: highlight.js covers ~85% of C# tokens. The audience sees code for seconds per slide -- perfect type/method highlighting matters less than clear keyword/string/comment differentiation
4. **Preserves features**: Line numbers, step animations, auto-animate, copy-code all continue working
5. **Already customized**: The presentation already has brand-colored token overrides; we just need to expand them

**Specific steps:**
1. Add CSS rules to differentiate types from keywords (teal vs blue)
2. Add CSS rules for `.hljs-title.function_`, `.hljs-variable`, `.hljs-property`, `.hljs-operator`
3. Optionally add `.hljs-title.class_` for class/type names in declarations
4. Consider switching base theme from Monokai to `vs2015` for slightly better C# defaults (though custom CSS overrides most of it anyway)

**If higher fidelity is needed later**, Shiki is the clear upgrade path, but it requires building a custom Reveal.js plugin that reimplements line numbering and step-through animations. This is a half-day to full-day effort and should only be pursued if the current highlighting is genuinely insufficient for the presentation's goals.

---

## Comparison Matrix

| Criteria | highlight.js (improved) | Shiki | Prism.js |
|----------|------------------------|-------|----------|
| C# quality | Good (keywords, strings, basic types) | Excellent (VS Code parity) | Good (C# 8.0 level) |
| Modern C# (12+) | Partial | Full | No (frozen at 8.0) |
| Reveal.js compat | Native | Custom plugin needed | Custom plugin needed |
| Line numbers | Built-in | Must reimplement | Must reimplement |
| Step animations | Built-in | Must reimplement | Must reimplement |
| Auto-animate | Built-in | Must reimplement | Must reimplement |
| Bundle size impact | 0 KB | ~600 KB (WASM + lang + theme) | ~30 KB |
| Performance | Instant | Async, noticeable delay | Fast |
| Maintenance | Active | Very active | Stalled |
| Integration effort | ~30 min (CSS only) | 4-8 hours (custom plugin) | 4-8 hours (custom plugin) |
| Theme customization | CSS classes (easy) | Inline styles (harder) | CSS classes (easy) |
