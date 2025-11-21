import { ListView } from "@progress/kendo-react-listview";
import { Pager } from "@progress/kendo-react-data-tools";
import { Card, CardBody, CardFooter, CardHeader } from "@progress/kendo-react-layout";
import type { Activity } from "../../../../Utils/interfaces";
import useChangePage from "../../../../Hooks/Common/useChangePage";
import ActivityItemRender from "./ActivityItemRenderer";

interface RecentActivityProps {
  recentActivity: Activity[];
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
          <div className="text-center text-text-secondary py-8">
            <p>No recent activity found.</p>
          </div>
        )}
      </CardBody>
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
    </Card>
  );
};

export default RecentActivity;
