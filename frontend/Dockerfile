# Use Bun base image for development with hot reload support
FROM oven/bun:1 AS development

WORKDIR /app

# Copy package files
COPY package.json bun.lock* ./

# Install dependencies
RUN bun install

# Copy the rest of the code
COPY . .

# Expose Vite dev server port
EXPOSE 5173

# Run development server with hot reload
CMD ["bun", "run", "dev", "--host", "0.0.0.0"]
