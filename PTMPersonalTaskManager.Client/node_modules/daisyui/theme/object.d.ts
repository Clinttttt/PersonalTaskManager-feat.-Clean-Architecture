interface Theme {
  "color-scheme": string
  "--color-base-100": string
  "--color-base-200": string
  "--color-base-300": string
  "--color-base-content": string
  "--color-primary": string
  "--color-primary-content": string
  "--color-secondary": string
  "--color-secondary-content": string
  "--color-accent": string
  "--color-accent-content": string
  "--color-neutral": string
  "--color-neutral-content": string
  "--color-info": string
  "--color-info-content": string
  "--color-success": string
  "--color-success-content": string
  "--color-warning": string
  "--color-warning-content": string
  "--color-error": string
  "--color-error-content": string
  "--radius-selector": string
  "--radius-field": string
  "--radius-box": string
  "--size-selector": string
  "--size-field": string
  "--border": string
  "--depth": string
  "--noise": string
}


interface Themes {
  winter: Theme
  fantasy: Theme
  garden: Theme
  cmyk: Theme
  cyberpunk: Theme
  light: Theme
  silk: Theme
  coffee: Theme
  lemonade: Theme
  night: Theme
  forest: Theme
  retro: Theme
  black: Theme
  valentine: Theme
  wireframe: Theme
  cupcake: Theme
  business: Theme
  corporate: Theme
  caramellatte: Theme
  autumn: Theme
  lofi: Theme
  dim: Theme
  dracula: Theme
  nord: Theme
  luxury: Theme
  bumblebee: Theme
  abyss: Theme
  synthwave: Theme
  emerald: Theme
  aqua: Theme
  pastel: Theme
  halloween: Theme
  dark: Theme
  acid: Theme
  sunset: Theme
  [key: string]: Theme
}

declare const themes: Themes
export default themes