import { useComposableResolver, useDataFetcher } from "#imports";

export default function() {
  const constraints = {
    "Composable": Composable(),
    "Is": Is(),
    "IsNot": IsNot()
  };

  async function evaluate({ constraint, value, contextData }) {
    if(!constraint) { return true; }

    const evaluator = constraints[constraint.type];

    return await evaluator.evaluate({ constraint, contextData, value });
  }

  return {
    evaluate
  };
}

function Composable() {
  const composableResolver = useComposableResolver();
  const dataFetcher = useDataFetcher();

  async function evaluate({ constraint, value, contextData }) {
    const options = constraint.options ? await dataFetcher.fetch({ data: constraint.options, contextData }) : { };
    const composable = composableResolver.resolve(constraint.composable).default(options);
    if(!composable.validate) { throw new Error("Constraint composable should have `validate`"); }

    return composable.validate(value);
  }

  return {
    evaluate
  };
}

function Is() {
  async function evaluate({ constraint, value }) {
    return constraint.is === value;
  }

  return {
    evaluate
  };
}

function IsNot() {
  async function evaluate({ constraint, value }) {
    return constraint.isNot !== value;
  }

  return {
    evaluate
  };
}
