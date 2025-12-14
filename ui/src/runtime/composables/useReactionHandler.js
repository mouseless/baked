import { watch } from "vue";
import { useConstraintEvaluator, useContext } from "#imports";

export default function() {
  const constraintEvaluator = useConstraintEvaluator();
  const context = useContext();

  function create(eventId, reactions) {
    const contextData = context.injectContextData();
    const events = context.injectEvents();

    const eventsToUnbind = [];

    function bind(reactionsDescriptor) {
      for(const reaction in reactionsDescriptor) {
        const trigger = reactionsDescriptor[reaction];
        const react = reactions[reaction];

        bindRecursive(trigger, react);
      }
    }

    function bindRecursive(trigger, react) {
      if(trigger.type === "On") {
        events.on(trigger.on, eventId, async value => {
          react(await constraintEvaluator.evaluate({ constraint: trigger.constraint, value, contextData }));
        });

        eventsToUnbind.push(trigger.on);
      } else if(trigger.type === "When") {
        watch(() => contextData.page[trigger.when], async value => {
          react(await constraintEvaluator.evaluate({ constraint: trigger.constraint, value, contextData }));
        }, { immediate: true });
      } else if(trigger.type === "Composite") {
        for(const part of trigger.parts) {
          bindRecursive(part, react);
        }
      }
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
