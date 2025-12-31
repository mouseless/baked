import { useError } from "#app";

export default function() {
  const error = useError();

  function computeSync() {
    return error;
  }

  return {
    computeSync
  };
}
