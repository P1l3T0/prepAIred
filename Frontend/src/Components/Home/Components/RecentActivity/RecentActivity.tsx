import { ListView } from "@progress/kendo-react-listview";
import { Pager } from "@progress/kendo-react-data-tools";
import { Card, CardBody, CardFooter, CardHeader } from "@progress/kendo-react-layout";
import useChangePage from "../../../../Hooks/Common/useChangePage";
import ActivityItemRender from "./ActivityItemRenderer";
import type { InterviewSessionActivity } from "../../../../Utils/interfaces";

interface RecentActivityProps {
  recentActivity: InterviewSessionActivity[];
}

const RecentActivity = ({ recentActivity }: RecentActivityProps) => {
  const { pagedData, skip, take, handlePageChange } = useChangePage(recentActivity);

  return (
    <Card className="border border-border shadow-sm shadow-primary h-full">
      <CardHeader className="border-border">
        <h3 className="text-xl font-medium">Recent Activity</h3>
      </CardHeader>
      <CardBody>
        {recentActivity.length > 0 ? (
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
            <h3 className="text-lg font-medium text-text-primary mb-2">No Recent Activity</h3>
            <p className="text-text-secondary">Start your first interview session to see your recent activity here.</p>
          </div>
        )}
      </CardBody>
      {recentActivity.length > 0 && (
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
