FROM mysql:8.0.33

ENV MYSQL_ROOT_PASSWORD=ecommerce
ENV MYSQL_DATABASE=ecommerce

COPY ./scripts/ /docker-entrypoint-initdb.d/

EXPOSE 3306