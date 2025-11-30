import FooterBrand from './FooterBrand';
import FooterLinks from './FooterLinks';
import FooterBottom from './FooterBottom';
import { platformLinks, supportLinks } from '../../../Utils/data';

const Footer = () => {
  return (
    <footer className="bg-surface border-t border-border mt-auto">
      <div className="max-w-7xl mx-auto px-6 py-12">
        <div className="grid grid-cols-1 md:grid-cols-4 gap-8">
          <FooterBrand />
          <FooterLinks title="Platform" links={platformLinks} />
          <FooterLinks title="Support" links={supportLinks} />
        </div>
        <FooterBottom />
      </div>
    </footer>
  );
};

export default Footer;