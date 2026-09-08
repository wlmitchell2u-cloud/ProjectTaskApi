const BASE_URL = 'https://localhost:7184/api';

async function request(method, url, data = null) {
  const options = {
    method,
    headers: {
      'Content-Type': 'application/json',
    },
  };

  if (data) {
    options.body = JSON.stringify(data);
  }

  const response = await fetch(`${BASE_URL}${url}`, options);

  console.log(response);

  if (!response.ok) {
    const errorText = await response.text();
    throw new Error(errorText || 'API Error: ${response.status}');
  }

  if (response.status === 204) return null;

  return response.json();
}

export const api = {
  get: (url) => request('GET', url),
  post: (url, data) => request('POST', url, data),
  put: (url, data) => request('PUT', url, data),
  delete: (url) => request('DELETE', url),
};
