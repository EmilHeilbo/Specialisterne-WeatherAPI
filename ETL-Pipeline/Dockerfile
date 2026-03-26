FROM ghcr.io/astral-sh/uv:python3.12-alpine

WORKDIR /app
RUN uv venv

COPY requirements.txt .
RUN uv pip install --no-cache-dir -r requirements.txt

COPY app ./app

CMD ["uv", "run", "-m", "app.main"]
