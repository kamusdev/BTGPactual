FROM mcr.microsoft.com/dotnet/sdk:8.0.402-bookworm-slim as build-image

ARG FUNCTION_DIR="/build"
ARG SAM_BUILD_MODE="run"
ENV PATH="/root/.dotnet/tools:${PATH}"

RUN apt-get update && apt-get -y install zip

ENV DOTNET_SYSTEM_NET_HTTP_USESOCKETSHTTPHANDLER=0

RUN mkdir $FUNCTION_DIR
WORKDIR $FUNCTION_DIR
COPY . $FUNCTION_DIR/
RUN dotnet tool install -g Amazon.Lambda.Tools --version 5.10.1

# Build and Copy artifacts depending on build mode.
RUN mkdir -p build_artifacts
RUN if [ "$SAM_BUILD_MODE" = "debug" ]; then dotnet lambda package --configuration Debug; else dotnet lambda package --configuration Release; fi
RUN if [ "$SAM_BUILD_MODE" = "debug" ]; then cp -r /build/bin/Debug/net8.0/publish/* /build/build_artifacts; else cp -r /build/bin/Release/net8.0/publish/* /build/build_artifacts; fi

FROM public.ecr.aws/lambda/dotnet:8

COPY --from=build-image /build/build_artifacts/ /var/task/
# Command can be overwritten by providing a different command in the template directly.
CMD ["FondosLambda::FondosLambda.EntryPoint::Run"]
