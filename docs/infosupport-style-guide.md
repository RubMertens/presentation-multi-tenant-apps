# Info Support Visual Identity - Style Guide

Extracted from https://www.infosupport.com/en/ (March 2026).

---

## Brand Colors

### Primary Palette

| Role             | Hex       | RGB              | Usage                                      |
|------------------|-----------|------------------|--------------------------------------------|
| Dark Blue        | `#003865` | rgb(0, 56, 101)  | Primary brand color, headers, dark backgrounds, icons, logo |
| Sky Blue         | `#00A3E0` | rgb(0, 163, 224) | Secondary brand color, links, accent borders, interactive elements |
| Light Blue       | `#6ECFF6` | rgb(110, 207, 246) | Light accent backgrounds, highlights       |
| Green Accent     | `#BED62F` | rgb(190, 214, 47) | Call-to-action buttons, primary button fill |
| Green Hover      | `#D0E26A` | rgb(208, 226, 106) | Button hover state                         |

### Neutral Palette

| Role             | Hex       | RGB               | Usage                                     |
|------------------|-----------|-------------------|--------------------------------------------|
| Body Text        | `#355670` | rgb(53, 86, 112)  | Default paragraph text                     |
| Dark Text        | `#212529` | rgb(33, 37, 41)   | Headings on light backgrounds              |
| White            | `#FFFFFF` | rgb(255, 255, 255)| Backgrounds, text on dark sections         |
| Light Gray       | `#F1F1F1` | rgb(241, 241, 241)| Subtle background sections                 |
| Gray             | `#D9D9D9` | rgb(217, 217, 217)| Borders, dividers                          |
| Black            | `#000000` | rgb(0, 0, 0)      | Rare, used sparingly                       |

### Background Section Classes (from website CSS)

| Class                    | Color     | Text Color |
|--------------------------|-----------|------------|
| `background-blue-dark`   | `#003865` | White      |
| `background-blue`        | `#00A3E0` | White      |
| `background-blue-light`  | `#6ECFF6` | Dark       |
| `background-green`       | `#BED62F` | Dark       |
| `background-white`       | `#FFFFFF` | Dark       |
| `background-gray`        | `#D9D9D9` | Dark       |
| `background-gray-light`  | `#F1F1F1` | Dark       |

---

## Typography

### Font Families

| Role           | Font Family        | Weight | Fallback   | Usage                           |
|----------------|--------------------|--------|------------|---------------------------------|
| Body text      | `GraphikRegular`   | 400    | serif      | Paragraphs, descriptions        |
| Body bold      | `GraphikBold`      | 700    | serif      | Emphasized body text            |
| Headings       | `PxGroteskBold`    | 700    | sans-serif | Section headings, titles        |
| Display/Hero   | `PxGroteskScreen`  | 900    | sans-serif | Hero titles, large display text |
| Regular grotesque | `PxGroteskRegular` | 400 | sans-serif | Subheadings, navigation         |
| Monospace      | `SFMono-Regular, Menlo, Monaco, Consolas` | -- | monospace | Code blocks |

### Font Sizes

| Token    | Size   | Usage                             |
|----------|--------|-----------------------------------|
| Base     | 1rem   | Body text (16px equivalent)       |
| Small    | 13px   | Captions, metadata                |
| Medium   | 20px   | Subheadings, lead text            |
| Large    | 36px   | Section headings                  |
| X-Large  | 42px   | Page titles, hero headings        |

### Line Height

- Body: `1.55`
- General: `1.5`

---

## Spacing and Sizing

### Spacing Scale (WordPress presets used on site)

| Token | Value    | Pixels (approx) |
|-------|----------|-----------------|
| 20    | 0.44rem  | ~7px            |
| 30    | 0.67rem  | ~11px           |
| 40    | 1rem     | 16px            |
| 50    | 1.5rem   | 24px            |
| 60    | 2.25rem  | 36px            |
| 70    | 3.38rem  | 54px            |
| 80    | 5.06rem  | ~81px           |

### Border Radius

| Usage         | Value       |
|---------------|-------------|
| Standard      | `.375rem`   |
| Buttons       | `.3125rem`  |
| Small         | `.25rem`    |
| Large         | `1rem`      |
| Pill          | `50rem`     |

---

## Buttons

### Default Button (CTA)
- Background: `#BED62F` (green accent)
- Border: `#BED62F`
- Text color: dark (inherited)
- Padding: `1rem 1.25rem`
- Border-radius: `.3125rem`
- Transition: `.4s ease`
- Hover: background `#D0E26A`
- Uses `:before` pseudo-element for animated hover effect

### Line Button (Secondary)
- Background: transparent
- Border: `#00A3E0` (sky blue)
- Font-size: `.75rem`
- Padding: `.6875rem .875rem`

### Simple Button (Text Link)
- Background: transparent
- No border
- Font-family: `GraphikRegular`
- Padding: 0
- Text-decoration: underline

---

## Design Patterns

### Page Layout
The site uses a section-based vertical layout:
1. **Header** - Logo top-left, navigation centered/right
2. **Hero/Proposition** - `background-blue-dark` (#003865), white text, green CTA button
3. **Content sections** - Alternating `background-white` and `background-blue-dark`
4. **Card grids** - Bootstrap grid (col-lg-4, col-lg-6), flexbox layouts
5. **Client logos** - Grid of partner/client logos on dark blue background
6. **Footer** - Dark section with navigation links, social icons, newsletter signup

### Section Rhythm
- Hero sections use dark blue (#003865) with white text
- Service/feature sections use white backgrounds with dark text
- Reference/case sections use dark blue (#003865) with white text
- CTA areas use green accent (#BED62F) buttons

### Card Patterns
- No heavy box shadows (clean/flat design)
- Subtle transitions on hover
- Content structured as: image/icon, heading, description, link
- Gutters: Bootstrap `--bs-gutter-x: 1.5rem`

### Icons and Logo
- Logo: SVG paths using `#00A3E0` (sky blue) for the logotype
- UI icons: SVG with `#003865` (dark blue) fill
- Decorative pixel grid icon (5 squares pattern) using `#003865`

---

## Presentation Application Guide

### Slide Backgrounds

| Slide Type      | Background   | Text Color | Accent         |
|-----------------|-------------|------------|----------------|
| Title slide     | `#003865`   | `#FFFFFF`  | `#00A3E0`      |
| Content slide   | `#FFFFFF`   | `#212529`  | `#003865`      |
| Section divider | `#003865`   | `#FFFFFF`  | `#BED62F`      |
| Highlight slide | `#00A3E0`   | `#FFFFFF`  | `#003865`      |
| Code slide      | `#F1F1F1`   | `#212529`  | `#003865`      |

### Text Hierarchy for Slides

| Element           | Font              | Size   | Color (light bg) | Color (dark bg) |
|-------------------|-------------------|--------|-------------------|-----------------|
| Slide title       | PxGroteskBold     | 42px   | `#003865`         | `#FFFFFF`       |
| Subtitle          | PxGroteskRegular  | 20px   | `#355670`         | `#6ECFF6`       |
| Body text         | GraphikRegular    | 16px   | `#355670`         | `#FFFFFF`       |
| Code blocks       | SFMono-Regular    | 14px   | `#212529`         | `#F1F1F1`       |
| Caption/small     | GraphikRegular    | 13px   | `#6C757D`         | `#D9D9D9`       |

### Accent Usage
- **Primary accent**: `#00A3E0` (sky blue) for links, highlighted terms, borders
- **CTA accent**: `#BED62F` (green) for buttons and call-to-action elements
- **Dividers/lines**: `#00A3E0` or `#BED62F` for horizontal rules and separators
- **Active/hover states**: lighter variants of the same color family

### Presentation Tips
- Use `#003865` dark blue as the dominant brand color for title slides and section dividers
- White text on dark blue sections; dark text on white sections
- Use `#BED62F` green sparingly for emphasis (buttons, key takeaways, bullet markers)
- Use `#00A3E0` sky blue for secondary accents (underlines, borders, diagrams)
- Keep backgrounds clean - avoid heavy gradients; prefer flat solid colors
- The brand feel is: **professional, clean, modern, technical, trustworthy**
- Border-radius should be subtle (`.3125rem` / 5px) - not overly rounded

### Font Substitutions (if custom fonts unavailable)
Since PxGrotesk and Graphik are commercial fonts, suitable alternatives:
- PxGroteskBold -> `"Source Sans Pro", "Segoe UI", sans-serif` (weight 700)
- PxGroteskScreen -> `"Source Sans Pro", "Segoe UI", sans-serif` (weight 900)
- GraphikRegular -> `"Inter", "Segoe UI", system-ui, sans-serif`

---

## Summary Color Swatches

```
Primary Dark Blue:  #003865  ████████
Sky Blue:           #00A3E0  ████████
Light Blue:         #6ECFF6  ████████
Green Accent:       #BED62F  ████████
Body Text:          #355670  ████████
White:              #FFFFFF  ████████
Light Gray:         #F1F1F1  ████████
Gray:               #D9D9D9  ████████
Dark Text:          #212529  ████████
```
