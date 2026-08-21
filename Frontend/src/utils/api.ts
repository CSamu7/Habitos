import type { ErrorAPI, Result } from "../types/common";

export async function betterFetch<TResponse>(
  url: string,
  requestInfo: RequestInit,
): Promise<Result<TResponse>> {
  if (requestInfo.method == "POST" || requestInfo.method == "PATCH") {
    requestInfo.headers = { "Content-type": "application/json" };
  }

  const request = await fetch(url, requestInfo);

  if (!request.ok) {
    //FIX: Puede haber un problema con solicitudes que traigan varios errores. ¿Podriamos pedir especificar el tipo de error?
    const errorBody: ErrorAPI = await request.json();

    return { error: errorBody };
  }

  const contentLength: string = request.headers.get("Content-Length") ?? "0";

  if (Number(contentLength) > 0) {
    const response = (await request.json()) as TResponse;

    return { data: response, error: null };
  }

  return { data: null as TResponse, error: null };
}
