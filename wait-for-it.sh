#!/usr/bin/env bash

host="$1"
shift
port="$1"
shift
cmd="$@"

until nc -z "$host" "$port"; do
  echo "Waiting service $host:$port..."
  sleep 3
done

>&2 echo "The service $host:$port is running!"
exec $cmd
