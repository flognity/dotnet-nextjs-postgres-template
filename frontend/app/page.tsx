/* SPDX-License-Identifier: MIT
 * Copyright (c) 2026 Florian Wilhelm
 * Description: Home page component for the frontend application.
 */
export default function HomePage() {
  return (
    <main className="mx-auto flex min-h-screen max-w-3xl flex-col justify-center px-6 py-12">
      <h1 className="text-3xl font-semibold tracking-tight text-slate-900">Template Web-App</h1>
      <p className="mt-4 text-lg text-slate-700">
        Starter frontend scaffold for a web application built with ASP.NET Core, Next.js, and PostgreSQL.
      </p>
      <p className="mt-2 text-sm text-slate-500">Next step: Implement core application features.</p>
    </main>
  );
}
