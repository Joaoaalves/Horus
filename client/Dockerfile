# Stage 1: Base build environment
FROM node:25-alpine AS base
WORKDIR /app
COPY package.json package-lock.json ./

# Install dependencies
RUN npm ci --legacy-peer-deps
COPY . .
# Install necessary dependencies for sharp (for image optimization)
RUN apk add --no-cache libc6-compat
# Stage 2: Development environment
FROM base AS development
ARG ENVIRONMENT=development
ENV NODE_ENV=$ENVIRONMENT
EXPOSE 3000
CMD ["npm", "run", "dev"]

# Stage 3: Production build
FROM base AS build
ARG ENVIRONMENT=production
ENV NODE_ENV=$ENVIRONMENT
RUN npm run build

# Stage 4: Production runtime environment
FROM node:25-alpine AS production
WORKDIR /app
COPY --from=build /app/.next ./.next
COPY --from=build /app/package.json ./package.json
COPY --from=build /app/package-lock.json ./package-lock.json
COPY --from=build /app/public ./public
COPY --from=build /app/node_modules ./node_modules
EXPOSE 3000
CMD ["npm", "run", "start"]