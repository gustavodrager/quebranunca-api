FROM postgres:15

COPY postgresql.conf /etc/postgresql/postgresql.conf
COPY pg_hba.conf /etc/postgresql/pg_hba.conf
COPY recovery.conf /etc/postgresql/recovery.conf

RUN chmod 600 /etc/postgresql/pg_hba.conf /etc/postgresql/recovery.conf

CMD ["postgres", "-c", "config_file=/etc/postgresql/postgresql.conf"]