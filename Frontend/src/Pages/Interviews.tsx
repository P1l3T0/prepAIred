import GenerateHrInterviews from "../Components/Interviews/HR/GenerateHrInterviews";
import GetHrInterviews from "../Components/Interviews/HR/GetHrInterviews";
import GenerateTechnicalInterviews from "../Components/Interviews/Technical/GenerateTechnicalInterviews";
import GetTechnicalInterviews from "../Components/Interviews/Technical/GetTechnicalInterviews";

const Interviews = () => {
  return (
    <>
      <GenerateHrInterviews />
      <GetHrInterviews />

      <GenerateTechnicalInterviews />
      <GetTechnicalInterviews />
    </>
  );
};

export default Interviews;
