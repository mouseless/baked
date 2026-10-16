import { watch, unref } from "vue";
import { useConstraintEvaluator, useContext, usePageContext } from "#imports";

export default function() {
  const constraintEvaluator = useConstraintEvaluator();
  const context = useContext();

  const events = context.injectEvents();
  const contextData = context.injectContextData();
  const pageContext = usePageContext();

  function create(eventId, reactions) {
    const handlers = {
      "On": On({ evaluate, events, eventId }),
      "When": When({ contextData, evaluate, pageContext }),
      "Composite": Composite({ parentBind: bindInternal })
    };

    async function evaluate(constraint, value) {
      return await constraintEvaluator.evaluate({
        constraint,
        value,
        contextData
      });
    }

    const eventsToUnbind = [];
    function bind(reactionsDescriptor) {
      for(const reaction in reactionsDescriptor) {
        const trigger = reactionsDescriptor[reaction];
        const react = reactions[reaction];

        eventsToUnbind.push(
          ...bindInternal({ trigger, react })
        );
      }
    }

    function bindInternal({ trigger, react }) {
      const handler = handlers[trigger.type];

      return handler.bind({ trigger, react });
    }

    function unbind() {
      for(const event of eventsToUnbind) {
        events.off(event, eventId);
      }
    }

    return {
      bind,
      unbind
    };
  }

  return {
    create
  };
}

function On({ evaluate, eventId, events }) {
  function bind({ trigger, react }) {
    events.on(trigger.on, eventId, async value => {
      react(await evaluate(trigger.constraint, value));
    });

    return [trigger.on];
  }

  return {
    bind
  };
}

function When({ contextData, evaluate, pageContext }) {
  function bind({ trigger, react }) {
    const key = trigger.key ?? "page";

    watch([
      () => access(contextData[key], trigger.when),
      () => key === "page" && pageContext.isPending(trigger.when.split(".")[0])
    ], async([value, pending]) => {
      react(false);
      if(pending) { return; }

      react(await evaluate(trigger.constraint, value));
    }, { immediate: true });

    return [];
  }
  function access(data, propChain) {
    data = unref(data);
    for(const prop of propChain.split(".")) {
      data = data?.[prop];
      data = unref(data);
    }

    return data;
  }

  return {
    bind
  };
}

function Composite({ parentBind }) {
  function bind({ trigger, react }) {
    const result = [];
    for(const part of trigger.parts) {
      result.push(
        ...parentBind({ trigger: part, react })
      );
    }

    return result;
  }

  return {
    bind
  };
}
