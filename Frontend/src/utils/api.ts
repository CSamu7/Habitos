import type { ErrorAPI, Result } from "../types/common";

export async function betterFetch<TResult>(
  url: string,
  requestInfo: RequestInit,
): Promise<Result<TResult>> {
  if (requestInfo.method == "POST" || requestInfo.method == "PATCH") {
    const jsonBody = JSON.stringify(requestInfo.body);
    requestInfo.body = jsonBody;
    requestInfo.headers = { "Content-type": "application/json" };
  }

  const request = await fetch(url, requestInfo);

  if (!request.ok) {
    //FIX: Puede haber un problema con solicitudes que traigan varios errores. ¿Podriamos pedir especificar el tipo de error?
    const errorBody: ErrorAPI = await request.json();

    return { error: errorBody };
  }

  const response: TResult = await request.json();

  return { data: response, error: null };
}
