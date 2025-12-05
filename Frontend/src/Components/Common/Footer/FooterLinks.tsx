interface FooterLinks {
  label: string;
  href: string;
}

interface FooterLinksProps {
  title: string;
  links: FooterLinks[];
}

const FooterLinks = ({ title, links }: FooterLinksProps) => {
  return (
    <div>
      <h4 className="text-text-primary font-semibold mb-4">{title}</h4>
      <ul className="space-y-3">
        {links.map((link, index) => (
          <li key={index}>
            <a href={link.href} className="text-text-secondary hover:text-primary text-sm transition-colors duration-200">
              {link.label}
            </a>
          </li>
        ))}
      </ul>
    </div>
  );
};

export default FooterLinks;