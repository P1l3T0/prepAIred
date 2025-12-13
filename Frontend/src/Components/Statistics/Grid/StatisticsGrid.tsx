import useFormatCells from "../../../Hooks/Statistics/Grid/useFormatCells";
import ColumnMenu from "./ColumnMenu";
import { Button } from "@progress/kendo-react-buttons";
import { CSVLink } from "react-csv";
import { ExcelExport } from "@progress/kendo-react-excel-export";
import { Grid, GridColumn, GridToolbar,type GridColumnMenuProps } from "@progress/kendo-react-grid";
import { fileExcelIcon, fileCsvIcon } from "@progress/kendo-svg-icons";
import useHandleExcelExport from "../../../Hooks/Statistics/Grid/useHandleExcelExport";
import useGetRecentInterviewSessions from "../../../Hooks/InterviewSessions/useGetRecentInterviewSessions";
import { Card, CardBody } from "@progress/kendo-react-layout";
import { Loader } from "@progress/kendo-react-indicators";

const StatisticsGrid = () => {
  const { data: interviewSessionData, isLoading, isError } = useGetRecentInterviewSessions();
  const { handleExcelExport, _excelExport } = useHandleExcelExport();
  const { StatusCell, ScoreCell, DateCell } = useFormatCells();

  const CustomColumnMenu = (props: GridColumnMenuProps) => (
    <ColumnMenu {...props} data={interviewSessionData!}></ColumnMenu>
  );

  if (isLoading) {
    return (
      <div className="bg-background flex items-center justify-center">
        <div className="flex flex-col items-center space-y-4 m-6">
          <Loader size="large" />
          <span className="text-text-secondary">Loading Interview Data...</span>
        </div>
      </div>
    );
  }

  if (isError) {
    return (
      <div className="bg-background flex items-center justify-center">
        <Card className="shadow-lg w-full m-6">
          <CardBody>
            <div className="text-center p-8">
              <h2 className="text-xl text-text-primary font-semibold mb-2">
                Unable to load Interview data
              </h2>
              <p className="text-text-secondary">
                Please try refreshing the page
              </p>
            </div>
          </CardBody>
        </Card>
      </div>
    );
  }

  return (
    <>
    {interviewSessionData && interviewSessionData.length > 0 ? (
      <ExcelExport fileName="Interviews" data={interviewSessionData} ref={_excelExport}>
        <Grid
          data={interviewSessionData}
          dataItemKey="id"
          adaptive={true}
          pageable={true}
          sortable={true}
          groupable={true}
          resizable={true}
          autoProcessData={true}
          pageSize={10}
          defaultTake={10}
        >
        <GridToolbar>
          <Button themeColor={'primary'} svgIcon={fileExcelIcon} onClick={handleExcelExport}>Export to Excel</Button>
          <Button themeColor={'primary'} svgIcon={fileCsvIcon}>
            <CSVLink filename="Interviews" data={interviewSessionData}>Export to CSV</CSVLink>
          </Button>
        </GridToolbar>
          <GridColumn field="id" title="ID"  width={50} columnMenu={CustomColumnMenu} />
          <GridColumn field="subject" title="Subject" columnMenu={CustomColumnMenu} />
          <GridColumn field="aiAgent" title="AI Agent" columnMenu={CustomColumnMenu} />
          <GridColumn field="programmingLanguage" title="Language" columnMenu={CustomColumnMenu} />
          <GridColumn field="position" title="Position" columnMenu={CustomColumnMenu} />
          <GridColumn field="averageScore" title="Score" cells={{ data: ScoreCell }} columnMenu={CustomColumnMenu}/>
          <GridColumn field="status" title="Status" cells={{ data: StatusCell }} columnMenu={CustomColumnMenu} />
          <GridColumn field="dateCreated" title="Date" cells={{ data: DateCell }} columnMenu={CustomColumnMenu} />
        </Grid>
      </ExcelExport>
    ) : (
      <div className="flex flex-col items-center justify-center text-center h-full py-12">
        <div className="mb-4">
          <svg 
            className="mx-auto h-16 w-16 text-text-tertiary" 
            fill="none" 
            viewBox="0 0 24 24" 
            stroke="currentColor"
          >
            <path 
              strokeLinecap="round" 
              strokeLinejoin="round" 
              strokeWidth={1.5} 
              d="M9 12h6m-6 4h6m2 5H7a2 2 0 01-2-2V5a2 2 0 012-2h5.586a1 1 0 01.707.293l5.414 5.414a1 1 0 01.293.707V19a2 2 0 01-2 2z" 
            />
          </svg>
        </div>
        <h3 className="text-lg font-medium text-text-primary mb-2">No Interview Sessions</h3>
        <p className="text-text-secondary">Start your first interview session to see your activities here.</p>
      </div>
    )}
    </>
  );
}

export default StatisticsGrid;