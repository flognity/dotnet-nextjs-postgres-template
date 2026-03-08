/* SPDX-License-Identifier: MIT
 * Copyright (c) 2026 Florian Wilhelm
 * Description: Tailwind CSS configuration for the frontend application.
 */
import type { Config } from 'tailwindcss';

const config: Config = {
  content: ['./app/**/*.{js,ts,jsx,tsx,mdx}'],
  theme: {
    extend: {}
  },
  plugins: []
};

export default config;
