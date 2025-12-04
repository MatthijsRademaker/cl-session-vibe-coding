#!/bin/bash

# Script to properly rebuild Docker containers after Dockerfile changes
# This ensures volume mounts work correctly with hot reload

echo "🛑 Stopping containers..."
docker compose down

echo "🗑️  Removing old images..."
docker compose rm -f

echo "🔨 Rebuilding images (no cache)..."
docker compose build --no-cache

echo "🚀 Starting containers..."
docker compose up -d

echo "📋 Showing logs (Ctrl+C to exit)..."
docker compose logs -f
