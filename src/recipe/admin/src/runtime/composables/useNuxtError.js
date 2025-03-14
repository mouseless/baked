import { useError } from "#app";

export default function() {
  const error = useError();

  function compute() {
    return error;
  }

  return {
    compute
  };
}
