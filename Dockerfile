FROM microsoft/aspnetcore-build as buil-env
WORKDIR /code
COPY *.csproj .
RUN dotnet restore
COPY *.* ./
RUN dotnet publish -c Release -o out

FROM microsoft/aspnetcore
WORKDIR /app
COPY --from=buil-env /code/out .
EXPOSE 5000
CMD [ "dotnet", "localmock.dll" ]

