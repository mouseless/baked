import { watch } from "vue";
import { useConstraintEvaluator, useContext } from "#imports";

export default function() {
  const constraintEvaluator = useConstraintEvaluator();
  const context = useContext();

  const events = context.injectEvents();
  const contextData = context.injectContextData();

  function create(eventId, reactions) {
    const handlers = {
      "On": On({ evaluate, events, eventId }),
      "When": When({ contextData, evaluate }),
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

function When({ contextData, evaluate }) {
  function bind({ trigger, react }) {
    watch(() => contextData.page[trigger.when], async value => {
      react(await evaluate(trigger.constraint, value));
    }, { immediate: true });

    return [];
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
