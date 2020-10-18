FROM registry.redhat.io/rhel8/dotnet-21 AS build-env
USER 0
COPY src/* ./
RUN chown -R 1001:0 /opt/app-root && fix-permissions /opt/app-root
USER 1001
RUN /usr/libexec/s2i/assemble
CMD /usr/libexec/s2i/run

# Build runtime image
FROM registry.redhat.io/rhel8/dotnet-21-runtime
USER 0
COPY --from=build-env /opt/app-root/app .
ENV ASPNETCORE_URLS=http://*:11130
LABEL io.openshift.expose-services="11130:http"
RUN chown -R 1001:0 /opt/app-root && fix-permissions /opt/app-root
USER 1001
ENTRYPOINT ["dotnet", "SecureApp.dll"]
