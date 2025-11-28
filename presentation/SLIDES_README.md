# Workshop Slides

Slidev presentation for the Vibecoding Workshop.

## Running the Slides

### Development Mode (Presenter View)

```bash
cd presentation
npm run slides
```

This opens:
- **Presenter mode**: http://localhost:3030/presenter
  - Your slides with speaker notes
  - Timer
  - Next slide preview

- **Slides**: http://localhost:3030
  - Clean slides for projection/screen share

### Build Static Site

```bash
cd presentation
cd presentation
npm run slides:build
```

Outputs to `dist/` - deploy to any static hosting.

### Export to PDF

```bash
cd presentation
cd presentation
npm run slides:export
```

Creates `slides-export.pdf` for sharing with participants.

---

## Presentation Structure

### Part 1: The Problem (15 min) - Slides 1-12
- Opening and workshop structure
- Exercise 1 free-form code review
- Discussion prompts

### Part 2: The Template (10 min) - Slides 13-18
- Template introduction
- Structure, quality, testing requirements

### Part 3: Hands-On (50 min) - Slides 19-27
- Assignment explanation
- Setup verification
- Build time with checkpoints

### Part 4: Discussion (20 min) - Slides 28-33
- Share experiences
- Metrics comparison
- Structure comparison

### Part 5: Honesty (15 min) - Slides 34-38
- When NOT to use LLMs
- Common assumptions
- Skill atrophy discussion
- Red flags

### Part 6: Wrap-Up (10 min) - Slides 39-43
- Key takeaways
- Next steps
- Resources
- Q&A

---

## Speaker Notes

Every slide includes speaker notes (visible in presenter mode) with:
- What to say
- Discussion prompts
- When to pause for questions
- Timing guidance

Press `?` in presenter mode to see keyboard shortcuts.

---

## Customization

Edit `slides.md` to customize:

1. **Branding**: Change theme/colors in frontmatter
2. **Content**: Update slides with your examples
3. **Timing**: Adjust based on your workshop length
4. **Code Examples**: Replace with your stack

### Themes

Change theme in `slides.md`:

```yaml
---
theme: default  # or: seriph, apple-basic, etc.
---
```

See [Slidev themes](https://sli.dev/themes/gallery.html)

---

## Tips for Presenting

### Before Workshop
1. Run slides in presenter mode
2. Test screen sharing with presenter notes visible to you
3. Have backup PDF exported
4. Test timer functionality

### During Workshop
- **Presenter mode shortcuts**:
  - `Space` / `→`: Next slide
  - `Shift + Space` / `←`: Previous slide
  - `o`: Toggle overview
  - `d`: Toggle dark mode
  - `g`: Go to slide number

### Timing Checkpoints
Slides 24-27 have timing checkpoints for hands-on section:
- 10 min
- 25 min
- 35 min
- 40 min

Use timer in presenter mode to stay on track.

---

## Features Used

- **Code highlighting**: Syntax highlighting for C#, TypeScript, Gherkin
- **Incremental reveals**: `<v-clicks>` for step-by-step content
- **Two-column layouts**: `layout: two-cols`
- **Speaker notes**: `<!-- notes -->`
- **Transitions**: Smooth slide transitions
- **Markdown**: Full MDC support

---

## Troubleshooting

### Slides won't start

```bash
# Reinstall dependencies
npm install
cd presentation
npm run slides
```

### Port already in use

```bash
# Use different port
npx slidev slides.md --port 3031
```

### PDF export fails

```bash
# Install playwright (required for PDF)
npx playwright install chromium
cd presentation
cd presentation
npm run slides:export
```

---

## Resources

- **Slidev docs**: https://sli.dev
- **Keyboard shortcuts**: Press `?` in slides
- **Theme gallery**: https://sli.dev/themes/gallery.html

---

*The slides are built with Slidev - a presentation framework for developers.*
