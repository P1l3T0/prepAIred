import type { TooltipContext } from "@progress/kendo-react-charts";

const useFormatPositionData = () => {
  const tooltipRender = (props: TooltipContext) =>
    `${props.point?.category}: ${props.point?.value} ${
      props.point?.value === 1 ? "session" : "sessions"
    }`;

  return tooltipRender;
};

export default useFormatPositionData;