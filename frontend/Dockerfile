# Use Bun base image for development with hot reload support
FROM oven/bun:1 AS development

WORKDIR /app

# Copy package files for dependency installation
COPY package.json bun.lock* ./

# Install dependencies (this is cached unless package files change)
RUN bun install

# DON'T copy source code here - it comes from the volume mount
# This ensures the container always uses the latest code from your host

# Expose Vite dev server port
EXPOSE 5173

# Run development server with hot reload
CMD ["bun", "run", "dev", "--host", "0.0.0.0"]
