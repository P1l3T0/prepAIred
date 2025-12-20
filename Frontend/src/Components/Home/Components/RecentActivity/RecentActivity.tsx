import { ListView } from "@progress/kendo-react-listview";
import { Pager } from "@progress/kendo-react-data-tools";
import { Card, CardBody, CardFooter, CardHeader } from "@progress/kendo-react-layout";
import useChangePage from "../../../../Hooks/Common/useChangePage";
import ActivityItemRender from "./ActivityItemRenderer";
import useGetRecentInterviewSessions from "../../../../Hooks/InterviewSessions/useGetRecentInterviewSessions";
import { Loader } from "@progress/kendo-react-indicators";

const RecentActivity = () => {
  const { data: recentActivity, isLoading, isError } = useGetRecentInterviewSessions();
  const { pagedData, skip, take, handlePageChange } = useChangePage(recentActivity || []);

  if (isLoading) {
    return (
      <div className="min-h-[calc(100vh-4.05rem)] sm:min-h-[calc(100vh-4.55rem)] bg-background flex items-center justify-center">
        <div className="flex flex-col items-center space-y-4">
          <Loader size="large" />
          <span className="text-text-secondary">Loading your profile...</span>
        </div>
      </div>
    );
  }

  if (isError) {
    return (
      <div className="min-h-[calc(100vh-4.05rem)] sm:min-h-[calc(100vh-4.55rem)] bg-background flex items-center justify-center">
        <Card className="shadow-lg">
          <CardBody>
            <div className="text-center p-8">
              <h2 className="text-xl text-text-primary font-semibold mb-2">
                Unable to load profile
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
    <Card className="border border-border shadow-md h-full">
      <CardHeader className="border-border">
        <h3 className="text-xl font-medium">Recent Activity</h3>
      </CardHeader>
      <CardBody>
        {recentActivity && recentActivity.length > 0 ? (
          <>
            <ListView data={pagedData} item={ActivityItemRender} />
          </>
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
            <h3 className="text-lg font-medium text-text-primary mb-2">
              No Recent Activity
            </h3>
            <p className="text-text-secondary">
              Start your first interview session to see your recent activity
              here.
            </p>
          </div>
        )}
      </CardBody>
      {recentActivity && recentActivity.length > 0 && (
        <CardFooter>
          <Pager
            size={"small"}
            className="k-listview-pager"
            skip={skip}
            take={take}
            onPageChange={handlePageChange}
            total={recentActivity.length}
          />
        </CardFooter>
      )}
    </Card>
  );
};

export default RecentActivity;
