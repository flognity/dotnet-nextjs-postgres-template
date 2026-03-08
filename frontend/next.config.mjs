/* SPDX-License-Identifier: MIT
 * Copyright (c) 2026 Florian Wilhelm
 * Description: Next.js configuration for the frontend build.
 */
// JSDoc type hint so editors know this object should match Next.js config shape
/** @type {import('next').NextConfig} */

const nextConfig = {
    reactStrictMode: true,  // Enable extra React checks in development to catch unsafe patterns early
    output: 'export'        // Build the site as static files in `out/` for hosting inside ASP.NET wwwroot
};

export default nextConfig;
