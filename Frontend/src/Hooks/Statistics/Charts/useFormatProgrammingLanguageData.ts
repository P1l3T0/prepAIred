import type { TooltipContext } from "@progress/kendo-react-charts";
import type { ProgrammingLanguageData } from "../../../Utils/interfaces";

const useFormatProgrammingLanguageData = (programmingLanguageData: ProgrammingLanguageData[]) => {
  const languages: string[] = programmingLanguageData?.map((item) => item.language) || [];
  const sessions: number[] = programmingLanguageData?.map((item) => item.sessions) || [];
  const tooltipRender = (props: TooltipContext) => `${props.point.value} ${props.point.value === 1 ? "session" : "sessions"}`;

  return { languages, sessions, tooltipRender };
};

export default useFormatProgrammingLanguageData;