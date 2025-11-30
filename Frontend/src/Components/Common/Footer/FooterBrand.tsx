import FooterSocialLinks from "./FooterSocialLinks";

const FooterBrand = () => {
  return (
    <div className="md:col-span-2">
      <div className="flex items-center space-x-2 mb-4">
        <h3 className="text-xl font-bold text-text-primary">
          <span className="text-primary">prep</span>AIred
        </h3>
      </div>
      <p className="text-text-secondary text-sm leading-relaxed mb-4 max-w-md">
        AI-powered interview preparation platform designed to help you ace your
        next job interview with confidence and expertise.
      </p>
      <FooterSocialLinks />
    </div>
  );
};

export default FooterBrand;