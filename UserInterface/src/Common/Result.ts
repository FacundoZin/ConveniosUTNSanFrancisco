export type Result<T> = Success<T> | Failure;

interface Success<T> {
  isSuccess: true;
  value: T;
  status: number;
}

interface Failure {
  isSuccess: false;
  error: {
    message: string;
    status: number;
  };
}

export const success = <T>(value: T, status: number): Success<T> => ({
  isSuccess: true,
  value,
  status,
});

export const failure = (message: string, status: number): Failure => ({
  isSuccess: false,
  error: {
    message,
    status,
  },
});